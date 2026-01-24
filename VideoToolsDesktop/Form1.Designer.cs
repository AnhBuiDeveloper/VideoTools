
namespace VideoToolsDesktop
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.lblVideo = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtSubtitle = new System.Windows.Forms.TextBox();
            this.btnBrowseSub = new System.Windows.Forms.Button();
            this.lblHeader2 = new System.Windows.Forms.Label();
            this.lblHardware = new System.Windows.Forms.Label();
            this.cmbHardware = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.chkUltrafast = new System.Windows.Forms.CheckBox();
            this.lblHeader3 = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.cmbFontName = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.lblMargin = new System.Windows.Forms.Label();
            this.numMarginV = new System.Windows.Forms.NumericUpDown();
            
            this.grpAdvanced = new System.Windows.Forms.GroupBox();
            this.chkBold = new System.Windows.Forms.CheckBox();
            this.chkItalic = new System.Windows.Forms.CheckBox();
            this.chkUnderline = new System.Windows.Forms.CheckBox();
            this.chkStrike = new System.Windows.Forms.CheckBox();
            this.lblTrans = new System.Windows.Forms.Label();
            this.trkTransparency = new System.Windows.Forms.TrackBar();
            this.lblTransVal = new System.Windows.Forms.Label();
            this.chkShadow = new System.Windows.Forms.CheckBox();
            this.lblSdw = new System.Windows.Forms.Label();
            this.numShadowWidth = new System.Windows.Forms.NumericUpDown();
            this.chkBorder = new System.Windows.Forms.CheckBox();
            this.lblBrd = new System.Windows.Forms.Label();
            this.numBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.btnBorderColor = new System.Windows.Forms.Button();

            this.lblPreview = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginV)).BeginInit();
            this.grpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numShadowWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            
            // FORM DEFAULTS
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35))))); 
            this.ForeColor = System.Drawing.Color.White; 
            this.ClientSize = new System.Drawing.Size(700, 900); // Slightly reduced height
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Tools Desktop (Advanced Styles)";
            this.Load += new System.EventHandler(this.Form1_Load);

            // --- 1. Source Files ---
            int y = 20;
            this.lblHeader1.AutoSize = true; this.lblHeader1.Location = new System.Drawing.Point(20, y); this.lblHeader1.Text = "1. Source Files"; this.lblHeader1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.lblHeader1.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            
            y += 30; // 50
            this.lblVideo.AutoSize = true; this.lblVideo.Location = new System.Drawing.Point(20, y+3); this.lblVideo.Text = "Video:"; 
            this.txtInput.Location = new System.Drawing.Point(100, y); this.txtInput.Size = new System.Drawing.Size(470, 23); this.txtInput.BackColor = System.Drawing.Color.FromArgb(50, 50, 60); this.txtInput.ForeColor = System.Drawing.Color.White; this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnBrowseInput.Location = new System.Drawing.Point(580, y-1); this.btnBrowseInput.Size = new System.Drawing.Size(80, 25); this.btnBrowseInput.Text = "..."; this.btnBrowseInput.BackColor = System.Drawing.Color.FromArgb(60, 60, 70); this.btnBrowseInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            
            y += 35; // 85
            this.lblSubtitle.AutoSize = true; this.lblSubtitle.Location = new System.Drawing.Point(20, y+3); this.lblSubtitle.Text = "Subtitle:"; 
            this.txtSubtitle.Location = new System.Drawing.Point(100, y); this.txtSubtitle.Size = new System.Drawing.Size(470, 23); this.txtSubtitle.BackColor = System.Drawing.Color.FromArgb(50, 50, 60); this.txtSubtitle.ForeColor = System.Drawing.Color.White; this.txtSubtitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnBrowseSub.Location = new System.Drawing.Point(580, y-1); this.btnBrowseSub.Size = new System.Drawing.Size(80, 25); this.btnBrowseSub.Text = "..."; this.btnBrowseSub.BackColor = System.Drawing.Color.FromArgb(60, 60, 70); this.btnBrowseSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnBrowseSub.Click += new System.EventHandler(this.btnBrowseSub_Click);

            // --- 2. Conversion Settings ---
            y += 40; // 125
            this.lblHeader2.AutoSize = true; this.lblHeader2.Location = new System.Drawing.Point(20, y); this.lblHeader2.Text = "2. Conversion Settings"; this.lblHeader2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.lblHeader2.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            
            y += 30; // 155
            this.lblHardware.AutoSize = true; this.lblHardware.Location = new System.Drawing.Point(20, y+3); this.lblHardware.Text = "Hardware:"; 
            this.cmbHardware.Location = new System.Drawing.Point(100, y); this.cmbHardware.Size = new System.Drawing.Size(200, 23);
            this.cmbHardware.Items.AddRange(new object[] { "Software (CPU)", "NVIDIA (NVENC)", "AMD (AMF)", "Intel (QSV)" }); /* ADDED */

            this.lblFormat.AutoSize = true; this.lblFormat.Location = new System.Drawing.Point(340, y+3); this.lblFormat.Text = "Format:"; 
            this.cmbFormat.Location = new System.Drawing.Point(400, y); this.cmbFormat.Size = new System.Drawing.Size(80, 23);
            this.cmbFormat.Items.AddRange(new object[] { "mkv", "mp4", "avi" }); /* ADDED */

            y += 35; // 190
            this.chkUltrafast.AutoSize = true; this.chkUltrafast.Location = new System.Drawing.Point(100, y); this.chkUltrafast.Text = "Ultrafast Preset (Max Speed)"; 

            // --- 3. Subtitle Style ---
            y += 40; // 230
            this.lblHeader3.AutoSize = true; this.lblHeader3.Location = new System.Drawing.Point(20, y); this.lblHeader3.Text = "3. Subtitle Style & Preview"; this.lblHeader3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.lblHeader3.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);

            y += 30; // 260
            this.lblFont.AutoSize = true; this.lblFont.Location = new System.Drawing.Point(20, y+3); this.lblFont.Text = "Font:"; 
            this.cmbFontName.Location = new System.Drawing.Point(100, y); this.cmbFontName.Size = new System.Drawing.Size(180, 23); this.cmbFontName.SelectedIndexChanged += new System.EventHandler(this.UI_Changed);
            this.cmbFontName.Items.AddRange(new object[] { "Arial", "Times New Roman", "Verdana", "Tahoma", "Courier New", "Comic Sans MS" }); /* ADDED */
            
            this.lblSize.AutoSize = true; this.lblSize.Location = new System.Drawing.Point(300, y+3); this.lblSize.Text = "Size:"; 
            this.numFontSize.Location = new System.Drawing.Point(340, y); this.numFontSize.Size = new System.Drawing.Size(60, 23); this.numFontSize.Minimum = 8; this.numFontSize.Value = 24; this.numFontSize.ValueChanged += new System.EventHandler(this.UI_Changed);

            y += 35; // 295
            this.lblColor.AutoSize = true; this.lblColor.Location = new System.Drawing.Point(20, y+5); this.lblColor.Text = "Color:"; 
            this.btnFontColor.Location = new System.Drawing.Point(100, y); this.btnFontColor.Size = new System.Drawing.Size(120, 27); this.btnFontColor.Text = "PICK COLOR"; this.btnFontColor.BackColor = System.Drawing.Color.White; this.btnFontColor.ForeColor = System.Drawing.Color.Black; this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            
            this.lblMargin.AutoSize = true; this.lblMargin.Location = new System.Drawing.Point(300, y+5); this.lblMargin.Text = "Bottom:"; 
            this.numMarginV.Location = new System.Drawing.Point(360, y+2); this.numMarginV.Size = new System.Drawing.Size(60, 23); this.numMarginV.Value = 30; this.numMarginV.ValueChanged += new System.EventHandler(this.UI_Changed);

            // --- Advanced Group ---
            y += 40; // 335
            this.grpAdvanced.Location = new System.Drawing.Point(20, y); 
            this.grpAdvanced.Size = new System.Drawing.Size(640, 150); 
            this.grpAdvanced.Text = "Advanced Styling"; this.grpAdvanced.ForeColor = System.Drawing.Color.White;

            // Row 1: Styles
            this.chkBold.Location = new System.Drawing.Point(20, 30); this.chkBold.Text = "Bold"; this.chkBold.AutoSize = true; this.chkBold.Checked = true; this.chkBold.CheckedChanged += new System.EventHandler(this.UI_Changed);
            this.chkItalic.Location = new System.Drawing.Point(100, 30); this.chkItalic.Text = "Italic"; this.chkItalic.AutoSize = true; this.chkItalic.CheckedChanged += new System.EventHandler(this.UI_Changed);
            this.chkUnderline.Location = new System.Drawing.Point(180, 30); this.chkUnderline.Text = "Underline"; this.chkUnderline.AutoSize = true; this.chkUnderline.CheckedChanged += new System.EventHandler(this.UI_Changed);
            this.chkStrike.Location = new System.Drawing.Point(280, 30); this.chkStrike.Text = "Strike"; this.chkStrike.AutoSize = true; this.chkStrike.CheckedChanged += new System.EventHandler(this.UI_Changed);
            
            this.lblTrans.Location = new System.Drawing.Point(380, 30); this.lblTrans.Text = "Alpha:"; this.lblTrans.AutoSize = true;
            this.trkTransparency.Location = new System.Drawing.Point(430, 25); this.trkTransparency.Size = new System.Drawing.Size(150, 45); this.trkTransparency.Maximum = 255; this.trkTransparency.TickStyle = System.Windows.Forms.TickStyle.None; this.trkTransparency.Scroll += new System.EventHandler(this.trkTransparency_Scroll);
            this.lblTransVal.Location = new System.Drawing.Point(590, 30); this.lblTransVal.Text = "0%"; this.lblTransVal.AutoSize = true;

            // Row 2: Shadow
            this.chkShadow.Location = new System.Drawing.Point(20, 70); this.chkShadow.Text = "Shadow"; this.chkShadow.AutoSize = true; this.chkShadow.Checked=true; this.chkShadow.CheckedChanged += new System.EventHandler(this.UI_Changed);
            this.lblSdw.Location = new System.Drawing.Point(100, 72); this.lblSdw.Text = "Width:"; this.lblSdw.AutoSize = true;
            this.numShadowWidth.Location = new System.Drawing.Point(150, 70); this.numShadowWidth.Value=1; this.numShadowWidth.ValueChanged += new System.EventHandler(this.UI_Changed);
            
            // Row 3: Border (FIXED OVERLAP)
            this.chkBorder.Location = new System.Drawing.Point(20, 110); this.chkBorder.Text = "Border"; this.chkBorder.AutoSize = true; this.chkBorder.Checked=true; this.chkBorder.CheckedChanged += new System.EventHandler(this.UI_Changed);
            this.lblBrd.Location = new System.Drawing.Point(100, 112); this.lblBrd.Text = "Width:"; this.lblBrd.AutoSize = true;
            this.numBorderWidth.Location = new System.Drawing.Point(150, 110); this.numBorderWidth.Value=1; this.numBorderWidth.ValueChanged += new System.EventHandler(this.UI_Changed);
            
            // MOVED Border Color Button to X=280 to fix overlap with NumericUpDown
            this.btnBorderColor.Location = new System.Drawing.Point(280, 108); this.btnBorderColor.Text = "Color"; this.btnBorderColor.Size = new System.Drawing.Size(60, 25); this.btnBorderColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnBorderColor.BackColor = System.Drawing.Color.Black; this.btnBorderColor.Click += new System.EventHandler(this.btnBorderColor_Click);

            this.grpAdvanced.Controls.Add(this.chkBold); this.grpAdvanced.Controls.Add(this.chkItalic); this.grpAdvanced.Controls.Add(this.chkUnderline); this.grpAdvanced.Controls.Add(this.chkStrike);
            this.grpAdvanced.Controls.Add(this.lblTrans); this.grpAdvanced.Controls.Add(this.trkTransparency); this.grpAdvanced.Controls.Add(this.lblTransVal);
            this.grpAdvanced.Controls.Add(this.chkShadow); this.grpAdvanced.Controls.Add(this.lblSdw); this.grpAdvanced.Controls.Add(this.numShadowWidth);
            this.grpAdvanced.Controls.Add(this.chkBorder); this.grpAdvanced.Controls.Add(this.lblBrd); this.grpAdvanced.Controls.Add(this.numBorderWidth); this.grpAdvanced.Controls.Add(this.btnBorderColor);

            y += 160; // 495
            this.lblPreview.AutoSize = true; this.lblPreview.Location = new System.Drawing.Point(20, y); this.lblPreview.Text = "Preview:"; 
            
            y += 25; // 520
            this.picPreview.Location = new System.Drawing.Point(20, y); this.picPreview.Size = new System.Drawing.Size(640, 150);
            
            y += 160; // 680
            // CONVERT BUTTON (FIXED TEXT VISIBILITY & CLICK EVENT)
            this.btnConvert.Location = new System.Drawing.Point(20, y); 
            this.btnConvert.Size = new System.Drawing.Size(640, 50); 
            this.btnConvert.Text = "START CONVERSION"; 
            this.btnConvert.BackColor = System.Drawing.Color.FromArgb(0, 122, 204); 
            this.btnConvert.ForeColor = System.Drawing.Color.White; // Ensure Visible
            this.btnConvert.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click); // ADDED MISSING HANDLER
            
            y += 60; // 740
            this.lblProgress.AutoSize = true; this.lblProgress.Location = new System.Drawing.Point(20, y); this.lblProgress.Text = "Ready"; 
            
            y += 25; // 765
            this.progressBar.Location = new System.Drawing.Point(20, y); this.progressBar.Size = new System.Drawing.Size(640, 20);
            
            y += 30; // 795
            this.txtLog.Location = new System.Drawing.Point(20, y); this.txtLog.Size = new System.Drawing.Size(640, 80); this.txtLog.BackColor = System.Drawing.Color.Black; this.txtLog.ForeColor = System.Drawing.Color.Lime;

            // Form Add
            this.Controls.Add(this.grpAdvanced);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.numMarginV);
            this.Controls.Add(this.lblMargin);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.numFontSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.cmbFontName);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.lblHeader3);
            this.Controls.Add(this.chkUltrafast);
            this.Controls.Add(this.cmbFormat);
            this.Controls.Add(this.lblFormat);
            this.Controls.Add(this.cmbHardware);
            this.Controls.Add(this.lblHardware);
            this.Controls.Add(this.lblHeader2);
            this.Controls.Add(this.btnBrowseSub);
            this.Controls.Add(this.txtSubtitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.lblHeader1);
            
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginV)).EndInit();
            this.grpAdvanced.ResumeLayout(false);
            this.grpAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numShadowWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Controls
        private System.Windows.Forms.Label lblHeader1;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtSubtitle;
        private System.Windows.Forms.Button btnBrowseSub;
        private System.Windows.Forms.Label lblHeader2;
        private System.Windows.Forms.Label lblHardware;
        private System.Windows.Forms.ComboBox cmbHardware;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.CheckBox chkUltrafast;
        private System.Windows.Forms.Label lblHeader3;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.ComboBox cmbFontName;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.NumericUpDown numMarginV;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox txtLog;
        
        // Advanced Controls
        private System.Windows.Forms.GroupBox grpAdvanced;
        private System.Windows.Forms.CheckBox chkBold;
        private System.Windows.Forms.CheckBox chkItalic;
        private System.Windows.Forms.CheckBox chkUnderline;
        private System.Windows.Forms.CheckBox chkStrike;
        private System.Windows.Forms.Label lblTrans;
        private System.Windows.Forms.TrackBar trkTransparency;
        private System.Windows.Forms.Label lblTransVal;
        private System.Windows.Forms.CheckBox chkShadow;
        private System.Windows.Forms.Label lblSdw;
        private System.Windows.Forms.NumericUpDown numShadowWidth; 
        private System.Windows.Forms.CheckBox chkBorder;
        private System.Windows.Forms.Label lblBrd;
        private System.Windows.Forms.NumericUpDown numBorderWidth;
        private System.Windows.Forms.Button btnBorderColor;
    }
}
