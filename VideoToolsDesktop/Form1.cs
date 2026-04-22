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
        private Color fontColor = Color.White;
        private Color borderColor = Color.Black;
        private TimeSpan totalDuration = TimeSpan.Zero;
        private Process? currentProcess = null;
        private bool isConverting = false;
        private bool isLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettingsAndApply();

            this.FormClosing += Form1_FormClosing;
            cmbHardware.SelectedIndexChanged += (s, ev) => { SaveCurrentSettings(); AutoSetOutputPath(); };
            cmbFormat.SelectedIndexChanged += (s, ev) => { SaveCurrentSettings(); AutoSetOutputPath(); };
            chkUltrafast.CheckedChanged += (s, ev) => SaveCurrentSettings();
            txtInput.TextChanged += (s, ev) => { SaveCurrentSettings(); AutoSetOutputPath(); };
            txtSubtitle.TextChanged += (s, ev) => SaveCurrentSettings();
            txtOutput.TextChanged += (s, ev) => SaveCurrentSettings();

            isLoaded = true;
            UpdatePreview();
        }

        private void LoadSettingsAndApply()
        {
            var s = SettingsManager.Load();

            if (s.HardwareIndex >= 0 && s.HardwareIndex < cmbHardware.Items.Count) cmbHardware.SelectedIndex = s.HardwareIndex;
            else cmbHardware.SelectedIndex = 0;

            if (s.FormatIndex >= 0 && s.FormatIndex < cmbFormat.Items.Count) cmbFormat.SelectedIndex = s.FormatIndex;
            else cmbFormat.SelectedIndex = 0;

            cmbFontName.SelectedItem = s.FontName;
            if (cmbFontName.SelectedIndex == -1) cmbFontName.SelectedIndex = 0;

            numFontSize.Value = s.FontSize;
            chkBold.Checked = s.IsBold;
            chkItalic.Checked = s.IsItalic;
            chkUnderline.Checked = s.IsUnderline;
            chkStrike.Checked = s.IsStrikeout;
            chkShadow.Checked = s.HasShadow;
            chkBorder.Checked = s.HasBorder;
            numShadowWidth.Value = s.ShadowWidth;
            numBorderWidth.Value = s.BorderWidth;
            numMarginV.Value = s.MarginV;

            trkTransparency.Value = s.Transparency;
            float pct = (trkTransparency.Value / 255f) * 100f;
            lblTransVal.Text = $"{pct:F0}%";

            chkUltrafast.Checked = s.IsUltrafast;
            fontColor = Color.FromArgb(s.FontColorArgb);
            borderColor = Color.FromArgb(s.BorderColorArgb);
            btnFontColor.BackColor = fontColor;
            btnBorderColor.BackColor = borderColor;

            txtInput.Text = s.InputPath;
            txtSubtitle.Text = s.SubtitlePath;
            txtOutput.Text = s.OutputPath;
        }

        private void SaveCurrentSettings()
        {
            if (!isLoaded) return;

            var s = new VideoToolsSettings
            {
                HardwareIndex = cmbHardware.SelectedIndex,
                FormatIndex = cmbFormat.SelectedIndex,
                FontName = cmbFontName.SelectedItem?.ToString() ?? "Arial",
                FontSize = numFontSize.Value,
                IsBold = chkBold.Checked,
                IsItalic = chkItalic.Checked,
                IsUnderline = chkUnderline.Checked,
                IsStrikeout = chkStrike.Checked,
                HasShadow = chkShadow.Checked,
                HasBorder = chkBorder.Checked,
                ShadowWidth = numShadowWidth.Value,
                BorderWidth = numBorderWidth.Value,
                MarginV = numMarginV.Value,
                Transparency = trkTransparency.Value,
                IsUltrafast = chkUltrafast.Checked,
                FontColorArgb = fontColor.ToArgb(),
                BorderColorArgb = borderColor.ToArgb(),
                InputPath = txtInput.Text,
                SubtitlePath = txtSubtitle.Text,
                OutputPath = txtOutput.Text
            };

            SettingsManager.Save(s);
        }

        private void AutoSetOutputPath()
        {
            if (!isLoaded) return;
            if (string.IsNullOrEmpty(txtInput.Text) || !File.Exists(txtInput.Text)) return;

            string fmt = cmbFormat.SelectedItem?.ToString() ?? "mp4";
            string baseName = Path.GetFileNameWithoutExtension(txtInput.Text);
            string dir = Path.GetDirectoryName(txtInput.Text)!;
            txtOutput.Text = Path.Combine(dir, $"{baseName}_converted.{fmt}");
        }

        private static string FindFfmpeg()
        {
            // Check bundled first
            string bundled = Path.Combine(AppContext.BaseDirectory, "ffmpeg.exe");
            if (File.Exists(bundled)) return bundled;

            // Check PATH
            foreach (string dir in (Environment.GetEnvironmentVariable("PATH") ?? "").Split(';'))
            {
                try
                {
                    string full = Path.Combine(dir.Trim(), "ffmpeg.exe");
                    if (File.Exists(full)) return full;
                }
                catch { }
            }

            return string.Empty;
        }

        private void UpdatePreview()
        {
            if (picPreview.Image != null) picPreview.Image.Dispose();

            Bitmap bmp = new Bitmap(picPreview.Width, picPreview.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.Clear(Color.DimGray);
                g.DrawString("PREVIEW", new Font("Arial", 8), Brushes.Gray, 5, 5);

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

                int alpha = 255 - trkTransparency.Value;
                Color primary = Color.FromArgb(alpha, fontColor);
                Color outline = Color.FromArgb(alpha, borderColor);

                using (f)
                using (Brush brushText = new SolidBrush(primary))
                using (Brush brushShadow = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                {
                    SizeF textSize = g.MeasureString(sampleText, f);
                    float x = (picPreview.Width - textSize.Width) / 2;
                    float y = picPreview.Height - textSize.Height - marginV;

                    if (chkShadow.Checked)
                    {
                        float offset = (float)numShadowWidth.Value;
                        g.DrawString(sampleText, f, brushShadow, x + offset, y + offset);
                    }

                    if (chkBorder.Checked)
                    {
                        float width = (float)numBorderWidth.Value;
                        if (width > 0)
                        {
                            using GraphicsPath path = new GraphicsPath();
                            using Pen pen = new Pen(outline, width * 2);
                            pen.LineJoin = LineJoin.Round;
                            path.AddString(sampleText, f.FontFamily, (int)style, g.DpiY * f.SizeInPoints / 72, new PointF(x, y), StringFormat.GenericDefault);
                            g.DrawPath(pen, path);
                        }
                    }

                    g.DrawString(sampleText, f, brushText, x, y);
                }
            }
            picPreview.Image = bmp;
        }

        // ASS format: &HAABBGGRR (00=Opaque, FF=Transparent)
        private static string ToAssColor(Color c, int alphaVal)
            => $"&H{alphaVal:X2}{c.B:X2}{c.G:X2}{c.R:X2}";

        private void btnBrowseInput_Click(object sender, EventArgs e) => BrowseFile(txtInput, "Video Files|*.mkv;*.mp4;*.avi;*.mov|All Files|*.*");
        private void btnBrowseSub_Click(object sender, EventArgs e) => BrowseFile(txtSubtitle, "Subtitle Files|*.srt|All Files|*.*");

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            string fmt = cmbFormat.SelectedItem?.ToString() ?? "mp4";
            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = $"{fmt.ToUpper()} Files|*.{fmt}|All Files|*.*";
            sfd.DefaultExt = fmt;
            if (!string.IsNullOrEmpty(txtOutput.Text))
            {
                sfd.InitialDirectory = Path.GetDirectoryName(txtOutput.Text);
                sfd.FileName = Path.GetFileName(txtOutput.Text);
            }
            if (sfd.ShowDialog() == DialogResult.OK)
                txtOutput.Text = sfd.FileName;
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (currentProcess != null && !currentProcess.HasExited)
            {
                try { currentProcess.Kill(); } catch { }
            }
        }

        private void UI_Changed(object sender, EventArgs e)
        {
            UpdatePreview();
            SaveCurrentSettings();
        }

        private void trkTransparency_Scroll(object sender, EventArgs e)
        {
            float pct = (trkTransparency.Value / 255f) * 100f;
            lblTransVal.Text = $"{pct:F0}%";
            UpdatePreview();
            SaveCurrentSettings();
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            using ColorDialog cd = new ColorDialog();
            cd.Color = fontColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                fontColor = cd.Color;
                btnFontColor.BackColor = fontColor;
                UpdatePreview();
                SaveCurrentSettings();
            }
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {
            using ColorDialog cd = new ColorDialog();
            cd.Color = borderColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                borderColor = cd.Color;
                btnBorderColor.BackColor = borderColor;
                UpdatePreview();
                SaveCurrentSettings();
            }
        }

        private void BrowseFile(TextBox target, string filter)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            if (ofd.ShowDialog() == DialogResult.OK) target.Text = ofd.FileName;
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
                    totalDuration = new TimeSpan(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
            }
            if (line.Contains("time=") && totalDuration.TotalSeconds > 0)
            {
                var match = Regex.Match(line, @"time=(\d{2}):(\d{2}):(\d{2})\.(\d{2})");
                if (match.Success)
                {
                    TimeSpan current = new TimeSpan(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                    double pct = Math.Min((current.TotalSeconds / totalDuration.TotalSeconds) * 100, 100);
                    progressBar.Value = (int)pct;
                    lblProgress.Text = $"Progress: {pct:F1}%";
                }
            }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (isConverting)
            {
                try { currentProcess?.Kill(); Log("Stopped."); } catch { }
                isConverting = false;
                btnConvert.Text = "START CONVERSION";
                btnConvert.BackColor = Color.FromArgb(0, 122, 204);
                return;
            }

            string inputFile = txtInput.Text;
            if (!File.Exists(inputFile)) { MessageBox.Show("Select a valid video file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            string outputFile = txtOutput.Text;
            if (string.IsNullOrWhiteSpace(outputFile))
            {
                AutoSetOutputPath();
                outputFile = txtOutput.Text;
            }
            if (string.IsNullOrWhiteSpace(outputFile)) { MessageBox.Show("Select an output file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            string ffmpegPath = FindFfmpeg();
            if (string.IsNullOrEmpty(ffmpegPath))
            {
                MessageBox.Show(
                    "ffmpeg.exe not found.\n\n" +
                    "Download from https://ffmpeg.org/download.html and place ffmpeg.exe:\n" +
                    $"  • Next to this app: {AppContext.BaseDirectory}\n" +
                    "  • Or add to system PATH",
                    "FFmpeg Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string? tempSubFile = null;
            string args = $"-i \"{inputFile}\" -sn ";

            if (File.Exists(txtSubtitle.Text))
            {
                string tempDir = Path.Combine(Path.GetTempPath(), "VideoToolsDesktop");
                Directory.CreateDirectory(tempDir);
                string safeName = "sub_" + Guid.NewGuid().ToString("N") + ".srt";
                tempSubFile = Path.Combine(tempDir, safeName);
                File.Copy(txtSubtitle.Text, tempSubFile, true);

                int alpha = trkTransparency.Value;
                string primColor = ToAssColor(fontColor, alpha);
                string outColor = ToAssColor(borderColor, alpha);
                string font = cmbFontName.SelectedItem?.ToString() ?? "Arial";
                int size = (int)numFontSize.Value;
                int margin = (int)numMarginV.Value;

                // ASS bold: -1 = true (VSFilter standard)
                int bold = chkBold.Checked ? -1 : 0;
                int italic = chkItalic.Checked ? 1 : 0;
                int uline = chkUnderline.Checked ? 1 : 0;
                int strike = chkStrike.Checked ? 1 : 0;
                int shadow = chkShadow.Checked ? (int)numShadowWidth.Value : 0;
                int outline = chkBorder.Checked ? (int)numBorderWidth.Value : 0;

                string style = $"FontName={font},FontSize={size},PrimaryColour={primColor},OutlineColour={outColor}";
                style += $",Bold={bold},Italic={italic},Underline={uline},StrikeOut={strike}";
                style += $",Shadow={shadow},Outline={outline},MarginV={margin},Alignment=2,BorderStyle=1";

                args += $"-vf \"subtitles='{safeName}':force_style='{style}'\" ";
            }

            args += "-c:a copy -map 0:v:0 -map 0:a ";

            if (cmbHardware.Text.Contains("NVIDIA"))
            {
                args += chkUltrafast.Checked
                    ? "-c:v h264_nvenc -preset p1 "
                    : "-c:v h264_nvenc -preset p6 -profile:v high -level 3.1 -rc vbr_hq -cq 20 -maxrate 6000k -bufsize 12000k -bf 3 -refs 4 ";
            }
            else if (cmbHardware.Text.Contains("AMD"))
            {
                args += chkUltrafast.Checked
                    ? "-c:v h264_amf -quality speed "
                    : "-c:v h264_amf -quality balanced ";
            }
            else if (cmbHardware.Text.Contains("Intel"))
            {
                args += chkUltrafast.Checked
                    ? "-c:v h264_qsv -preset veryfast "
                    : "-c:v h264_qsv -preset medium ";
            }
            else
            {
                args += chkUltrafast.Checked
                    ? "-c:v libx264 -preset ultrafast "
                    : "-c:v libx264 -preset slow -crf 21 -profile:v high -pix_fmt yuv420p ";
            }

            args += $"\"{outputFile}\" -y";

            progressBar.Value = 0;
            txtLog.Clear();
            Log($"FFmpeg: {ffmpegPath}");
            Log($"Args: {args}");

            isConverting = true;
            btnConvert.Text = "STOP CONVERSION";
            btnConvert.BackColor = Color.Red;

            string tempDir2 = tempSubFile != null ? Path.GetDirectoryName(tempSubFile)! : Path.GetDirectoryName(inputFile)!;

            await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        WorkingDirectory = tempDir2
                    };
                    using Process p = new Process();
                    currentProcess = p;
                    p.StartInfo = psi;
                    p.ErrorDataReceived += (s, data) => { if (data.Data != null) Log(data.Data); };
                    p.Start();
                    p.BeginErrorReadLine();
                    p.WaitForExit();

                    this.Invoke(new Action(() =>
                    {
                        if (isConverting)
                        {
                            if (p.ExitCode == 0)
                                MessageBox.Show($"Conversion complete!\n\nOutput: {outputFile}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show($"FFmpeg exited with code {p.ExitCode}.\nCheck the log for details.", "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        isConverting = false;
                        btnConvert.Text = "START CONVERSION";
                        btnConvert.BackColor = Color.FromArgb(0, 122, 204);
                    }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        Log("Error: " + ex.Message);
                        MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isConverting = false;
                        btnConvert.Text = "START CONVERSION";
                        btnConvert.BackColor = Color.FromArgb(0, 122, 204);
                    }));
                }
                finally
                {
                    if (!string.IsNullOrEmpty(tempSubFile) && File.Exists(tempSubFile))
                        try { File.Delete(tempSubFile); } catch { }
                }
            });
        }
    }
}
