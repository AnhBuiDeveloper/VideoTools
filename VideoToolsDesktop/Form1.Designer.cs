using System.Drawing;
using System.Windows.Forms;

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
            this.lblAppTitle = new Label();
            this.lblAppSubtitle = new Label();
            this.pnlSource = new RoundedPanel();
            this.lblHeader1 = new Label();
            this.lblVideo = new Label();
            this.txtInput = new TextBox();
            this.btnBrowseInput = new Button();
            this.lblSubtitle = new Label();
            this.txtSubtitle = new TextBox();
            this.btnBrowseSub = new Button();
            this.lblOutput = new Label();
            this.txtOutput = new TextBox();
            this.btnBrowseOutput = new Button();
            this.pnlConversion = new RoundedPanel();
            this.lblHeader2 = new Label();
            this.lblHardware = new Label();
            this.cmbHardware = new ComboBox();
            this.lblFormat = new Label();
            this.cmbFormat = new ComboBox();
            this.chkUltrafast = new CheckBox();
            this.pnlStyle = new RoundedPanel();
            this.lblHeader3 = new Label();
            this.lblFont = new Label();
            this.cmbFontName = new ComboBox();
            this.lblSize = new Label();
            this.numFontSize = new NumericUpDown();
            this.lblColor = new Label();
            this.btnFontColor = new Button();
            this.lblMargin = new Label();
            this.numMarginV = new NumericUpDown();
            this.grpAdvanced = new GroupBox();
            this.chkBold = new CheckBox();
            this.chkItalic = new CheckBox();
            this.chkUnderline = new CheckBox();
            this.chkStrike = new CheckBox();
            this.lblTrans = new Label();
            this.trkTransparency = new TrackBar();
            this.lblTransVal = new Label();
            this.chkShadow = new CheckBox();
            this.lblSdw = new Label();
            this.numShadowWidth = new NumericUpDown();
            this.chkBorder = new CheckBox();
            this.lblBrd = new Label();
            this.numBorderWidth = new NumericUpDown();
            this.btnBorderColor = new Button();
            this.pnlPreview = new RoundedPanel();
            this.lblPreview = new Label();
            this.picPreview = new PictureBox();
            this.btnConvert = new Button();
            this.lblProgress = new Label();
            this.progressBar = new ProgressBar();
            this.txtLog = new RichTextBox();

            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginV)).BeginInit();
            this.grpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numShadowWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();

            Color page = Color.FromArgb(13, 18, 30);
            Color card = Color.FromArgb(21, 30, 45);
            Color field = Color.FromArgb(11, 18, 31);
            Color border = Color.FromArgb(45, 59, 78);
            Color text = Color.FromArgb(235, 241, 248);
            Color muted = Color.FromArgb(148, 163, 184);
            Color accent = Color.FromArgb(14, 165, 233);
            Color accentGreen = Color.FromArgb(34, 197, 94);

            // FORM
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = page;
            this.ClientSize = new Size(820, 716);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.ForeColor = text;
            this.MinimumSize = new Size(760, 676);
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Video Tools Desktop";
            this.Load += new System.EventHandler(this.Form1_Load);

            // APP HEADER
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblAppTitle.ForeColor = Color.White;
            this.lblAppTitle.Location = new Point(22, 14);
            this.lblAppTitle.Text = "Video Tools Desktop";

            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblAppSubtitle.ForeColor = muted;
            this.lblAppSubtitle.Location = new Point(24, 48);
            this.lblAppSubtitle.Text = "Convert videos and burn styled subtitles with FFmpeg.";

            // SOURCE CARD
            ConfigureCard(this.pnlSource, card, border, new Point(20, 76), new Size(780, 132));
            ConfigureSectionHeader(this.lblHeader1, "01  Source Files", accent, new Point(18, 12));

            ConfigureLabel(this.lblVideo, "Video", muted, new Point(18, 42));
            ConfigureTextBox(this.txtInput, field, text, border, new Point(96, 38), new Size(560, 26));
            ConfigureButton(this.btnBrowseInput, "Browse", Color.FromArgb(30, 41, 59), text, border, new Point(674, 37), new Size(82, 28));
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);

            ConfigureLabel(this.lblSubtitle, "Subtitle", muted, new Point(18, 72));
            ConfigureTextBox(this.txtSubtitle, field, text, border, new Point(96, 68), new Size(560, 26));
            ConfigureButton(this.btnBrowseSub, "Browse", Color.FromArgb(30, 41, 59), text, border, new Point(674, 67), new Size(82, 28));
            this.btnBrowseSub.Click += new System.EventHandler(this.btnBrowseSub_Click);

            ConfigureLabel(this.lblOutput, "Output", muted, new Point(18, 102));
            ConfigureTextBox(this.txtOutput, field, text, border, new Point(96, 98), new Size(560, 26));
            ConfigureButton(this.btnBrowseOutput, "Browse", Color.FromArgb(30, 41, 59), text, border, new Point(674, 97), new Size(82, 28));
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);

            this.pnlSource.Controls.AddRange(new Control[] {
                this.lblHeader1, this.lblVideo, this.txtInput, this.btnBrowseInput,
                this.lblSubtitle, this.txtSubtitle, this.btnBrowseSub,
                this.lblOutput, this.txtOutput, this.btnBrowseOutput
            });

            // CONVERSION CARD
            ConfigureCard(this.pnlConversion, card, border, new Point(20, 220), new Size(780, 82));
            ConfigureSectionHeader(this.lblHeader2, "02  Conversion Settings", accent, new Point(18, 12));

            ConfigureLabel(this.lblHardware, "Hardware", muted, new Point(18, 48));
            ConfigureCombo(this.cmbHardware, field, text, new Point(96, 43), new Size(210, 28));
            this.cmbHardware.Items.AddRange(new object[] { "Software (CPU)", "NVIDIA (NVENC)", "AMD (AMF)", "Intel (QSV)" });

            ConfigureLabel(this.lblFormat, "Format", muted, new Point(346, 48));
            ConfigureCombo(this.cmbFormat, field, text, new Point(408, 43), new Size(104, 28));
            this.cmbFormat.Items.AddRange(new object[] { "mkv", "mp4", "avi" });

            ConfigureCheckBox(this.chkUltrafast, "Ultrafast preset", text, new Point(570, 46));

            this.pnlConversion.Controls.AddRange(new Control[] {
                this.lblHeader2, this.lblHardware, this.cmbHardware,
                this.lblFormat, this.cmbFormat, this.chkUltrafast
            });

            // STYLE CARD
            ConfigureCard(this.pnlStyle, card, border, new Point(20, 314), new Size(780, 194));
            ConfigureSectionHeader(this.lblHeader3, "03  Subtitle Style", accent, new Point(18, 12));

            ConfigureLabel(this.lblFont, "Font", muted, new Point(18, 48));
            ConfigureCombo(this.cmbFontName, field, text, new Point(96, 43), new Size(178, 28));
            this.cmbFontName.Items.AddRange(new object[] { "Arial", "Times New Roman", "Verdana", "Tahoma", "Courier New", "Comic Sans MS" });
            this.cmbFontName.SelectedIndexChanged += new System.EventHandler(this.UI_Changed);

            ConfigureLabel(this.lblSize, "Size", muted, new Point(302, 48));
            ConfigureNumeric(this.numFontSize, field, text, new Point(346, 43), new Size(64, 28));
            this.numFontSize.Minimum = 8;
            this.numFontSize.Value = 24;
            this.numFontSize.ValueChanged += new System.EventHandler(this.UI_Changed);

            ConfigureLabel(this.lblColor, "Color", muted, new Point(436, 48));
            ConfigureButton(this.btnFontColor, "Text Color", Color.White, Color.FromArgb(15, 23, 42), border, new Point(486, 41), new Size(96, 30));
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);

            ConfigureLabel(this.lblMargin, "Bottom", muted, new Point(616, 48));
            ConfigureNumeric(this.numMarginV, field, text, new Point(680, 43), new Size(64, 28));
            this.numMarginV.Value = 30;
            this.numMarginV.ValueChanged += new System.EventHandler(this.UI_Changed);

            this.grpAdvanced.BackColor = card;
            this.grpAdvanced.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.grpAdvanced.ForeColor = muted;
            this.grpAdvanced.Location = new Point(18, 78);
            this.grpAdvanced.Size = new Size(738, 100);
            this.grpAdvanced.Text = " Advanced styling ";

            ConfigureCheckBox(this.chkBold, "Bold", text, new Point(18, 28));
            this.chkBold.Checked = true;
            this.chkBold.CheckedChanged += new System.EventHandler(this.UI_Changed);
            ConfigureCheckBox(this.chkItalic, "Italic", text, new Point(86, 28));
            this.chkItalic.CheckedChanged += new System.EventHandler(this.UI_Changed);
            ConfigureCheckBox(this.chkUnderline, "Underline", text, new Point(154, 28));
            this.chkUnderline.CheckedChanged += new System.EventHandler(this.UI_Changed);
            ConfigureCheckBox(this.chkStrike, "Strike", text, new Point(258, 28));
            this.chkStrike.CheckedChanged += new System.EventHandler(this.UI_Changed);

            ConfigureLabel(this.lblTrans, "Alpha", muted, new Point(382, 29));
            this.trkTransparency.BackColor = card;
            this.trkTransparency.Location = new Point(430, 23);
            this.trkTransparency.Maximum = 255;
            this.trkTransparency.Size = new Size(152, 45);
            this.trkTransparency.TickStyle = TickStyle.None;
            this.trkTransparency.Scroll += new System.EventHandler(this.trkTransparency_Scroll);
            ConfigureLabel(this.lblTransVal, "0%", text, new Point(598, 29));

            ConfigureCheckBox(this.chkShadow, "Shadow", text, new Point(18, 72));
            this.chkShadow.Checked = true;
            this.chkShadow.CheckedChanged += new System.EventHandler(this.UI_Changed);
            ConfigureLabel(this.lblSdw, "Width", muted, new Point(106, 74));
            ConfigureNumeric(this.numShadowWidth, field, text, new Point(154, 70), new Size(62, 24));
            this.numShadowWidth.Value = 1;
            this.numShadowWidth.ValueChanged += new System.EventHandler(this.UI_Changed);

            ConfigureCheckBox(this.chkBorder, "Border", text, new Point(258, 72));
            this.chkBorder.Checked = true;
            this.chkBorder.CheckedChanged += new System.EventHandler(this.UI_Changed);
            ConfigureLabel(this.lblBrd, "Width", muted, new Point(338, 74));
            ConfigureNumeric(this.numBorderWidth, field, text, new Point(386, 70), new Size(62, 24));
            this.numBorderWidth.Value = 1;
            this.numBorderWidth.ValueChanged += new System.EventHandler(this.UI_Changed);
            ConfigureButton(this.btnBorderColor, "Border Color", Color.Black, Color.White, border, new Point(474, 68), new Size(106, 28));
            this.btnBorderColor.Click += new System.EventHandler(this.btnBorderColor_Click);

            this.grpAdvanced.Controls.AddRange(new Control[] {
                this.chkBold, this.chkItalic, this.chkUnderline, this.chkStrike,
                this.lblTrans, this.trkTransparency, this.lblTransVal,
                this.chkShadow, this.lblSdw, this.numShadowWidth,
                this.chkBorder, this.lblBrd, this.numBorderWidth, this.btnBorderColor
            });

            this.pnlStyle.Controls.AddRange(new Control[] {
                this.lblHeader3, this.lblFont, this.cmbFontName, this.lblSize, this.numFontSize,
                this.lblColor, this.btnFontColor, this.lblMargin, this.numMarginV, this.grpAdvanced
            });

            // PREVIEW CARD
            ConfigureCard(this.pnlPreview, card, border, new Point(20, 520), new Size(780, 120));
            ConfigureSectionHeader(this.lblPreview, "Preview", accentGreen, new Point(18, 12));

            this.picPreview.BackColor = Color.FromArgb(31, 41, 55);
            this.picPreview.Location = new Point(18, 38);
            this.picPreview.Size = new Size(738, 66);
            this.picPreview.SizeMode = PictureBoxSizeMode.StretchImage;

            this.pnlPreview.Controls.AddRange(new Control[] { this.lblPreview, this.picPreview });

            // ACTIONS AND STATUS
            ConfigureButton(this.btnConvert, "START CONVERSION", accent, Color.White, accent, new Point(20, 652), new Size(780, 36));
            this.btnConvert.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);

            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblProgress.ForeColor = text;
            this.lblProgress.Location = new Point(20, 696);
            this.lblProgress.Text = "Ready";

            this.progressBar.Location = new Point(84, 697);
            this.progressBar.Size = new Size(250, 14);
            this.progressBar.Style = ProgressBarStyle.Continuous;

            this.txtLog.BackColor = Color.FromArgb(2, 6, 23);
            this.txtLog.BorderStyle = BorderStyle.None;
            this.txtLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.txtLog.ForeColor = Color.FromArgb(134, 239, 172);
            this.txtLog.Location = new Point(352, 692);
            this.txtLog.Size = new Size(448, 20);

            // Form Controls
            this.Controls.Add(this.lblAppTitle);
            this.Controls.Add(this.lblAppSubtitle);
            this.Controls.Add(this.pnlSource);
            this.Controls.Add(this.pnlConversion);
            this.Controls.Add(this.pnlStyle);
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtLog);

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

        private static void ConfigureCard(RoundedPanel panel, Color backColor, Color borderColor, Point location, Size size)
        {
            panel.BackColor = backColor;
            panel.BorderColor = borderColor;
            panel.BorderWidth = 1;
            panel.CornerRadius = 8;
            panel.Location = location;
            panel.Size = size;
        }

        private static void ConfigureSectionHeader(Label label, string text, Color color, Point location)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label.ForeColor = color;
            label.Location = location;
            label.Text = text;
        }

        private static void ConfigureLabel(Label label, string text, Color color, Point location)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            label.ForeColor = color;
            label.Location = location;
            label.Text = text;
        }

        private static void ConfigureTextBox(TextBox textBox, Color backColor, Color foreColor, Color borderColor, Point location, Size size)
        {
            textBox.BackColor = backColor;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBox.ForeColor = foreColor;
            textBox.Location = location;
            textBox.Size = size;
        }

        private static void ConfigureCombo(ComboBox comboBox, Color backColor, Color foreColor, Point location, Size size)
        {
            comboBox.BackColor = backColor;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox.ForeColor = foreColor;
            comboBox.Location = location;
            comboBox.Size = size;
        }

        private static void ConfigureNumeric(NumericUpDown numeric, Color backColor, Color foreColor, Point location, Size size)
        {
            numeric.BackColor = backColor;
            numeric.BorderStyle = BorderStyle.FixedSingle;
            numeric.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            numeric.ForeColor = foreColor;
            numeric.Location = location;
            numeric.Size = size;
        }

        private static void ConfigureCheckBox(CheckBox checkBox, string text, Color foreColor, Point location)
        {
            checkBox.AutoSize = true;
            checkBox.FlatStyle = FlatStyle.Flat;
            checkBox.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox.ForeColor = foreColor;
            checkBox.Location = location;
            checkBox.Text = text;
            checkBox.UseVisualStyleBackColor = true;
        }

        private static void ConfigureButton(Button button, string text, Color backColor, Color foreColor, Color borderColor, Point location, Size size)
        {
            button.BackColor = backColor;
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderColor = borderColor;
            button.FlatAppearance.BorderSize = 1;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            button.ForeColor = foreColor;
            button.Location = location;
            button.Size = size;
            button.Text = text;
            button.UseVisualStyleBackColor = false;
        }

        #endregion

        private Label lblAppTitle;
        private Label lblAppSubtitle;
        private RoundedPanel pnlSource;
        private RoundedPanel pnlConversion;
        private RoundedPanel pnlStyle;
        private RoundedPanel pnlPreview;
        private Label lblHeader1;
        private Label lblVideo;
        private TextBox txtInput;
        private Button btnBrowseInput;
        private Label lblSubtitle;
        private TextBox txtSubtitle;
        private Button btnBrowseSub;
        private Label lblOutput;
        private TextBox txtOutput;
        private Button btnBrowseOutput;
        private Label lblHeader2;
        private Label lblHardware;
        private ComboBox cmbHardware;
        private Label lblFormat;
        private ComboBox cmbFormat;
        private CheckBox chkUltrafast;
        private Label lblHeader3;
        private Label lblFont;
        private ComboBox cmbFontName;
        private Label lblSize;
        private NumericUpDown numFontSize;
        private Label lblColor;
        private Button btnFontColor;
        private Label lblMargin;
        private NumericUpDown numMarginV;
        private Label lblPreview;
        private PictureBox picPreview;
        private Button btnConvert;
        private Label lblProgress;
        private ProgressBar progressBar;
        private RichTextBox txtLog;

        private GroupBox grpAdvanced;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkUnderline;
        private CheckBox chkStrike;
        private Label lblTrans;
        private TrackBar trkTransparency;
        private Label lblTransVal;
        private CheckBox chkShadow;
        private Label lblSdw;
        private NumericUpDown numShadowWidth;
        private CheckBox chkBorder;
        private Label lblBrd;
        private NumericUpDown numBorderWidth;
        private Button btnBorderColor;
    }
}
