using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VideoToolsDesktop
{
    public partial class Form1 : Form
    {
        // State
        private Color fontColor = Color.White;
        private Color borderColor = Color.Black;
        private TimeSpan totalDuration = TimeSpan.Zero;
        private Process currentProcess = null;
        private bool isConverting = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Defaults
            cmbHardware.SelectedIndex = 0;
            cmbFormat.SelectedIndex = 0;
            cmbFontName.SelectedItem = "Arial";
            
            // Set Button Colors
            btnFontColor.BackColor = fontColor;
            btnBorderColor.BackColor = borderColor;
            
            UpdatePreview();
        }

        // --- Logic ---

        private void UpdatePreview()
        {
            if (picPreview.Image != null) picPreview.Image.Dispose();

            Bitmap bmp = new Bitmap(picPreview.Width, picPreview.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // 1. Background
                g.Clear(Color.DimGray);
                g.DrawString("PREVIEW", new Font("Arial", 8), Brushes.Gray, 5, 5);

                // 2. Resolve Styles
                string sampleText = "This is a sample subtitle text 123";
                string fontName = cmbFontName.SelectedItem?.ToString() ?? "Arial";
                float fontSize = (float)numFontSize.Value;
                int marginV = (int)numMarginV.Value;

                FontStyle style = FontStyle.Regular;
                if (chkBold.Checked) style |= FontStyle.Bold;
                if (chkItalic.Checked) style |= FontStyle.Italic;
                if (chkUnderline.Checked) style |= FontStyle.Underline;
                if (chkStrike.Checked) style |= FontStyle.Strikeout;

                Font f;
                try { f = new Font(fontName, fontSize, style); }
                catch { f = new Font("Arial", fontSize, style); }

                // 3. Resolve Colors
                int alpha = 255 - trkTransparency.Value; // 0=Opaque
                Color primaryRaw = fontColor;
                Color primary = Color.FromArgb(alpha, primaryRaw);
                Color outlineRaw = borderColor;
                Color outline = Color.FromArgb(alpha, outlineRaw);

                using (f)
                using (Brush brushText = new SolidBrush(primary))
                using (Brush brushShadow = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                {
                    SizeF textSize = g.MeasureString(sampleText, f);
                    float x = (picPreview.Width - textSize.Width) / 2;
                    float y = picPreview.Height - textSize.Height - marginV;

                    // 4. Draw Shadow
                    if (chkShadow.Checked)
                    {
                        float offset = (float)numShadowWidth.Value;
                        g.DrawString(sampleText, f, brushShadow, x + offset, y + offset);
                    }

                    // 5. Draw Border (Outline) using GraphicsPath
                    if (chkBorder.Checked)
                    {
                        float width = (float)numBorderWidth.Value;
                        if (width > 0)
                        {
                            using (GraphicsPath path = new GraphicsPath())
                            using (Pen pen = new Pen(outline, width * 2)) // Pen width is center-aligned, so x2 covers
                            {
                                pen.LineJoin = LineJoin.Round;
                                path.AddString(sampleText, f.FontFamily, (int)style, g.DpiY * f.SizeInPoints / 72, new PointF(x, y), StringFormat.GenericDefault);
                                g.DrawPath(pen, path);
                            }
                        }
                    }

                    // 6. Draw Text Fill
                    g.DrawString(sampleText, f, brushText, x, y);
                }
            }
            picPreview.Image = bmp;
        }

        // --- Helper: Convert Color to ASS Hex ---
        // ASS Format: &HAABBGGRR (Alpha is inverted: 00=Opaque, FF=Transparent)
        private string ToAssColor(Color c, int alphaVal)
        {
            // alphaVal is 0-255 (from trackbar where 0=Opaque usually in UI logic, but let's check)
            // In UI: 0% Transparecy = 255 Alpha. Trackbar usually 0-100%. 
            // My Trackbar is 0-255. 0 = Opaque.
            // ASS Alpha: 00 = Opaque, FF = Transparent. So Trackbar value maps DIRECTLY to ASS Alpha.
            
            return $"&H{alphaVal:X2}{c.B:X2}{c.G:X2}{c.R:X2}";
        }

        // --- Event Handlers ---

        private void btnBrowseInput_Click(object sender, EventArgs e) => BrowseFile(txtInput, "Video Files|*.mkv;*.mp4;*.avi;*.mov|All Files|*.*");
        private void btnBrowseSub_Click(object sender, EventArgs e) => BrowseFile(txtSubtitle, "Subtitle Files|*.srt|All Files|*.*");
        private void UI_Changed(object sender, EventArgs e) => UpdatePreview();

        private void trkTransparency_Scroll(object sender, EventArgs e)
        {
            float pct = (trkTransparency.Value / 255f) * 100f;
            lblTransVal.Text = $"{pct:F0}%";
            UpdatePreview();
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = fontColor;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    fontColor = cd.Color;
                    btnFontColor.BackColor = fontColor;
                    UpdatePreview();
                }
            }
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = borderColor;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    borderColor = cd.Color;
                    btnBorderColor.BackColor = borderColor;
                    UpdatePreview();
                }
            }
        }

        private void BrowseFile(TextBox target, string filter)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = filter;
                if (ofd.ShowDialog() == DialogResult.OK) target.Text = ofd.FileName;
            }
        }

        private void Log(string msg)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<string>(Log), msg); return; }
            txtLog.AppendText(msg + Environment.NewLine);
            txtLog.ScrollToCaret();
            ParseProgress(msg);
        }

        private void ParseProgress(string line)
        {
            if (line.Contains("Duration:"))
            {
                var match = Regex.Match(line, @"Duration: (\d{2}):(\d{2}):(\d{2})\.(\d{2})");
                if (match.Success)
                {
                    totalDuration = new TimeSpan(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                    lblProgress.Text = $"Total Duration: {totalDuration}";
                }
            }
            if (line.Contains("time=") && totalDuration.TotalSeconds > 0)
            {
                var match = Regex.Match(line, @"time=(\d{2}):(\d{2}):(\d{2})\.(\d{2})");
                if (match.Success)
                {
                    TimeSpan current = new TimeSpan(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                    double pct = (current.TotalSeconds / totalDuration.TotalSeconds) * 100;
                    if (pct > 100) pct = 100;
                    progressBar.Value = (int)pct;
                    lblProgress.Text = $"Progress: {pct:F1}%";
                }
            }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (isConverting)
            {
                try { currentProcess?.Kill(); Log("Killed."); } catch { }
                isConverting = false; btnConvert.Text = "START CONVERSION"; btnConvert.BackColor = Color.FromArgb(0, 122, 204);
                return;
            }

            string inputFile = txtInput.Text;
            if (!File.Exists(inputFile)) { MessageBox.Show("Select video file."); return; }

            string fmt = cmbFormat.SelectedItem.ToString();
            string outputFile = Path.ChangeExtension(inputFile, $"_converted_advanced.{fmt}");
            string ffmpegPath = "ffmpeg"; 

            string args = $"-i \"{inputFile}\" -sn ";

            if (File.Exists(txtSubtitle.Text))
            {
                string subPath = txtSubtitle.Text.Replace("\\", "/").Replace(":", "\\:");
                
                // Construct Force Style String
                // Colors: &HAABBGGRR
                int alpha = trkTransparency.Value;
                string primColor = ToAssColor(fontColor, alpha);
                string outColor = ToAssColor(borderColor, alpha); // Usually border keeps same alpha or solid? Let's use same.
                
                string font = cmbFontName.SelectedItem?.ToString() ?? "Arial";
                int size = (int)numFontSize.Value;
                int margin = (int)numMarginV.Value;
                
                int bold = chkBold.Checked ? -1 : 0; // ASS uses -1 for true sometimes, but 1 works too. FFMPEG doc says 1. Let's use 1. Actually VSFilter uses -1. Let's stick to 1 for safety.
                int italic = chkItalic.Checked ? 1 : 0;
                int uline = chkUnderline.Checked ? 1 : 0;
                int strike = chkStrike.Checked ? 1 : 0;
                
                int shadow = chkShadow.Checked ? (int)numShadowWidth.Value : 0;
                int outline = chkBorder.Checked ? (int)numBorderWidth.Value : 0;

                string style = $"FontName={font},FontSize={size},PrimaryColour={primColor},OutlineColour={outColor}";
                style += $",Bold={bold},Italic={italic},Underline={uline},StrikeOut={strike}";
                style += $",Shadow={shadow},Outline={outline},MarginV={margin}";
                
                // Alignment=2 (Bottom Center) is default
                style += ",Alignment=2,BorderStyle=1";

                args += $"-vf \"subtitles='{subPath}':force_style='{style}'\" ";
            }

            // 1. Audio & Mapping
            // -c:a copy: Copy audio stream without re-encoding
            // -map 0:v:0: Select first video stream from input
            // -map 0:a: Select all audio streams from input
            args += "-c:a copy -map 0:v:0 -map 0:a ";

            // 2. Video Encoder & Presets
            string encoder = "libx264";
            
            if (cmbHardware.Text.Contains("NVIDIA"))
            {
                encoder = "h264_nvenc";
                args += $"-c:v {encoder} ";

                if (chkUltrafast.Checked)
                {
                    // Speed Mode
                    args += "-preset p1 "; 
                }
                else
                {
                    // Quality Mode (Constrained VBR Level 3.1)
                    // P6, VBR HQ, CQ 20, Max 6M, Buf 12M, BF 3, Refs 4
                    args += "-preset p6 -profile:v high -level 3.1 -rc vbr_hq -cq 20 -maxrate 6000k -bufsize 12000k -bf 3 -refs 4 ";
                }
            }
            else if (cmbHardware.Text.Contains("AMD"))
            {
                 encoder = "h264_amf";
                 args += $"-c:v {encoder} ";
                 if (chkUltrafast.Checked) args += "-quality speed ";
                 else args += "-quality balanced ";
            }
            else if (cmbHardware.Text.Contains("Intel"))
            {
                 encoder = "h264_qsv";
                 args += $"-c:v {encoder} ";
                 if (chkUltrafast.Checked) args += "-preset veryfast ";
                 else args += "-preset medium ";
            }
            else // CPU (Software)
            {
                 args += "-c:v libx264 ";
                 if (chkUltrafast.Checked) 
                 {
                     args += "-preset ultrafast ";
                 }
                 else 
                 {
                     // Quality Mode (Refined Request)
                     // Libx264: slow, crf 21, profile high
                     args += "-preset slow -crf 21 -profile:v high ";
                 }
            }

            args += $"\"{outputFile}\" -y";

            progressBar.Value = 0;
            txtLog.Clear();
            Log($"CMD: {ffmpegPath} {args}");

            isConverting = true;
            btnConvert.Text = "STOP CONVERSION";
            btnConvert.BackColor = Color.Red;

            await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo { FileName = ffmpegPath, Arguments = args, UseShellExecute = false, RedirectStandardError = true, CreateNoWindow = true };
                    using (Process p = new Process())
                    {
                        currentProcess = p;
                        p.StartInfo = psi;
                        p.ErrorDataReceived += (s, data) => { if (data.Data != null) Log(data.Data); };
                        p.Start();
                        p.BeginErrorReadLine();
                        p.WaitForExit();
                        this.Invoke(new Action(() =>
                        {
                            if (isConverting) MessageBox.Show(p.ExitCode == 0 ? "Success!" : "Failed!");
                            isConverting = false; btnConvert.Text = "START CONVERSION"; btnConvert.BackColor = Color.FromArgb(0, 122, 204);
                        }));
                    }
                }
                catch (Exception ex) { this.Invoke(new Action(() => { Log("Err: " + ex.Message); isConverting = false; })); }
            });
        }
    }
}
