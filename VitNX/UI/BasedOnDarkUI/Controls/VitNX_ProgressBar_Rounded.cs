using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNX_ProgressBarRounded : ProgressBar
    {
        [Description("Font of the text on ProgressBar"), Category("Additional Options")]
        public Font TextFont { get; set; } = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold);

        private SolidBrush _textColourBrush = (SolidBrush)Brushes.Black;

        [Category("Additional Options")]
        public Color TextColor
        { get { return _textColourBrush.Color; } set { _textColourBrush.Dispose(); _textColourBrush = new SolidBrush(value); } }

        private SolidBrush _progressColourBrush = (SolidBrush)Brushes.DodgerBlue;

        [Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ProgressColor
        { get { return _progressColourBrush.Color; } set { _progressColourBrush.Dispose(); _progressColourBrush = new SolidBrush(value); } }

        private VitNX_ProgressBarDisplayMode _visualMode = VitNX_ProgressBarDisplayMode.CurrProgress;

        [Category("Additional Options"), Browsable(true)]
        public VitNX_ProgressBarDisplayMode VisualMode
        { get { return _visualMode; } set { _visualMode = value; Invalidate(); } }

        private string _text = string.Empty;

        [Description("If it's empty, % will be shown"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string CustomText
        { get { return _text; } set { _text = value; Invalidate(); } }

        private string _textToDraw
        {
            get
            {
                string text = CustomText;
                switch (VisualMode)
                {
                    case VitNX_ProgressBarDisplayMode.Percentage:
                        text = _percentageStr;
                        break;

                    case VitNX_ProgressBarDisplayMode.CurrProgress:
                        text = _currProgressStr;
                        break;

                    case VitNX_ProgressBarDisplayMode.TextAndCurrProgress:
                        text = $"{CustomText}: {_currProgressStr}";
                        break;

                    case VitNX_ProgressBarDisplayMode.TextAndPercentage:
                        text = $"{CustomText}: {_percentageStr}";
                        break;
                }
                return text;
            }
            set { }
        }

        public VitNX_ProgressBarRounded()
        { Value = Minimum; FixComponentBlinking(); }

        private string _currProgressStr
        { get { return $"{Value}/{Maximum}"; } }

        private void FixComponentBlinking()
        { SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); }

        private string _percentageStr
        { get { return $"{(int)((float)Value - Minimum) / ((float)Maximum - Minimum) * 100 } %"; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            IntPtr hrgn = Functions.Windows.Win32.Import.CreateRoundRectRgn(0, 0, Width, Height, 7, 7); // 0, 0, Width, Height, [x], [x] ; x - degree of roundness of the control (set depending on the size of the control on the form), recommended = 4
            Region = Region.FromHrgn(hrgn);
            Graphics g = e.Graphics; DrawProgressBar(g); DrawStringIfNeeded(g);
        }

        private void DrawProgressBar(Graphics g)
        {
            Rectangle rect = ClientRectangle;
            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            if (Value > 0) { Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round((float)Value / Maximum * rect.Width), rect.Height); g.FillRectangle(_progressColourBrush, clip); }
        }

        private void DrawStringIfNeeded(Graphics g)
        {
            if (VisualMode != VitNX_ProgressBarDisplayMode.NoText)
            {
                string text = _textToDraw;
                SizeF len = g.MeasureString(text, TextFont);
                Point location = new Point((Width / 2) - (int)len.Width / 2, (Height / 2) - (int)len.Height / 2);
                g.DrawString(text, TextFont, _textColourBrush, location);
            }
        }

        public new void Dispose()
        { _textColourBrush.Dispose(); _progressColourBrush.Dispose(); base.Dispose(); }

        private void InitializeComponent()
        { SuspendLayout(); BackColor = Color.FromArgb(69, 73, 74); ResumeLayout(false); }
    }
}