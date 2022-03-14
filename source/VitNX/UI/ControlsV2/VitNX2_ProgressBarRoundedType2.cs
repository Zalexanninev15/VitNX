using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VitNX.UI.ControlsV2
{
    public partial class VitNX2_ProgressBarRoundedType2 : PictureBox
    {
        private Timer t = new Timer();
        private double pbUnit;
        private int pbWIDTH, pbHEIGHT, pbComplete;
        private Bitmap bmp;
        private Graphics g;

        public VitNX2_ProgressBarRoundedType2()
        {
            DoubleBuffered = true;
            ProgressBarColor = Color.FromArgb(224, 224, 224);
            ProgressBackColor = Color.FromArgb(255, 128, 255);
            ProgressFont = new Font(Font.FontFamily,
                (int)(Height * 0.7),
                FontStyle.Bold);
            ProgressFontColor = Color.Black;
            Value = 0;
        }

        public int Value { get; set; }

        [Category("Appearance")]
        public Color ProgressBarColor { get; set; }

        [Category("Appearance")]
        public Color ProgressBackColor { get; set; }

        [Category("Appearance")]
        public Font ProgressFont { get; set; }

        [Category("Appearance")]
        public Color ProgressFontColor { get; set; }

        private GraphicsPath GetRoundRectagle(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = bounds.Height;
            if (bounds.Height <= 0) radius = 20;
            path.AddArc(bounds.X,
                bounds.Y,
                radius,
                radius,
                180,
                90);
            path.AddArc(bounds.X + bounds.Width - radius,
                bounds.Y,
                radius,
                radius,
                270,
                90);
            path.AddArc(bounds.X + bounds.Width - radius,
                bounds.Y + bounds.Height - radius,
                        radius,
                        radius,
                        0,
                        90);
            path.AddArc(bounds.X,
                bounds.Y + bounds.Height - radius,
                radius,
                radius,
                90,
                90);
            path.CloseAllFigures();
            return path;
        }

        private void RecreateRegion()
        {
            var bounds = new Rectangle(ClientRectangle.Location,
                ClientRectangle.Size);
            bounds.Inflate(-1, -1);
            Region = new Region(GetRoundRectagle(bounds));
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RecreateRegion();
        }

        /// <summary>
        /// Starts the animation of filling the progress bar
        /// </summary>
        public void Animate()
        {
            pbWIDTH = Width;
            pbHEIGHT = Height;
            pbUnit = pbWIDTH / 100.0;
            pbComplete = 0;
            bmp = new Bitmap(pbWIDTH,
                pbHEIGHT);
            t.Interval = 50;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(ProgressBackColor);
            GraphicsPath path = new GraphicsPath();
            Rectangle innerBounds = new Rectangle(0,
                0,
                (int)(pbComplete * pbUnit),
                pbHEIGHT);
            Region r = new Region(GetRoundRectagle(innerBounds));
            g.FillRegion(new SolidBrush(ProgressBarColor), r);
            g.DrawString(pbComplete + "%",
                new Font(ProgressFont.FontFamily,
                (int)(pbHEIGHT * 0.6),
                ProgressFont.Style),
                new SolidBrush(ProgressFontColor),
                new PointF(pbWIDTH / 2 - pbHEIGHT,
                pbHEIGHT / 10));
            Image = bmp;
            pbComplete++;
            if (pbComplete > Value)
            {
                g.Dispose();
                t.Stop();
            }
        }
    }
}