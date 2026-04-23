using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VideoToolsDesktop
{
    internal sealed class RoundedPanel : Panel
    {
        [DefaultValue(8)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CornerRadius { get; set; } = 8;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor { get; set; } = Color.FromArgb(42, 52, 68);

        [DefaultValue(1)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderWidth { get; set; } = 1;

        public RoundedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using GraphicsPath path = CreatePath(ClientRectangle, CornerRadius);
            using SolidBrush background = new SolidBrush(BackColor);
            e.Graphics.FillPath(background, path);

            if (BorderWidth > 0)
            {
                using Pen border = new Pen(BorderColor, BorderWidth);
                e.Graphics.DrawPath(border, path);
            }
        }

        protected override void OnResize(System.EventArgs eventargs)
        {
            base.OnResize(eventargs);
            using GraphicsPath path = CreatePath(ClientRectangle, CornerRadius);
            Region = new Region(path);
        }

        private static GraphicsPath CreatePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Rectangle rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
