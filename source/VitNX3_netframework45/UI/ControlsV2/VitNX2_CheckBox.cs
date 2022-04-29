using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using VitNX2.UI.ControlsV2.UiKit;

namespace VitNX2.UI.ControlsV2
{
    [DefaultEvent("CheckedChanged")]
    public class VitNX2_CheckBox : Control
    {
        protected override void OnTextChanged(EventArgs e)
        {
            OnTextChanged(e);
            Invalidate();
        }

        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        public event CheckedChangedEventHandler CheckedChanged;

        protected override void OnClick(EventArgs e)
        {
            _Checked = !_Checked;
            if (CheckedChanged != null)
                CheckedChanged(this);
            OnClick(e);
        }

        [Category("Appearance")]
        public _Options Options
        {
            get { return O; }
            set { O = value; }
        }

        protected override void OnResize(EventArgs e)
        {
            OnResize(e);
            Height = 22;
        }

        [Category("Color")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        [Category("Color")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        public VitNX2_CheckBox()
        {
            SetStyle(ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.FromArgb(60, 70, 73);
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 10f);
            Size = new Size(112, 22);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateColors();
            Bitmap bitmap = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            W = Width - 1;
            H = Height - 1;
            Rectangle rect = new Rectangle(0, 2,
                Height - 5,
                Height - 5);
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(BackColor);
            _Options o = O;
            if (o != _Options.Style1)
            {
                if (o == _Options.Style2)
                {
                    graphics2.FillRectangle(new SolidBrush(_BaseColor), rect);
                    MouseState state = State;
                    if (state != MouseState.Over)
                    {
                        if (state == MouseState.Down)
                        {
                            graphics2.DrawRectangle(new Pen(_BorderColor), rect);
                            graphics2.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), rect);
                        }
                    }
                    else
                    {
                        graphics2.DrawRectangle(new Pen(_BorderColor), rect);
                        graphics2.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), rect);
                    }
                    if (Checked)
                    {
                        graphics2.DrawString("ü",
             new Font("Wingdings", 18f),
             new SolidBrush(_BorderColor),
             new Rectangle(5, 7, H - 9, H - 9),
             Helpers.CenterSF);
                    }
                    if (!Enabled)
                    {
                        graphics2.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)),
                            rect);
                        graphics2.DrawString(Text, Font,
                            new SolidBrush(Color.FromArgb(47, 110, 165)),
                            new Rectangle(20, 2, W, H),
                            Helpers.NearSF);
                    }
                    graphics2.DrawString(Text, Font,
                        new SolidBrush(_TextColor),
                        new Rectangle(20, 2, W, H),
                        Helpers.NearSF);
                }
            }
            else
            {
                graphics2.FillRectangle(new SolidBrush(_BaseColor), rect);
                MouseState state = State;
                if (state != MouseState.Over)
                {
                    if (state == MouseState.Down)
                        graphics2.DrawRectangle(new Pen(_BorderColor), rect);
                }
                else
                    graphics2.DrawRectangle(new Pen(_BorderColor), rect);
                if (Checked)
                {
                    graphics2.DrawString("ü",
         new Font("Wingdings", 18f),
         new SolidBrush(_BorderColor),
         new Rectangle(5, 7, H - 9, H - 9),
         Helpers.CenterSF);
                }
                if (!Enabled)
                {
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), rect);
                    graphics2.DrawString(Text,
                        Font,
                        new SolidBrush(Color.FromArgb(140, 142, 143)),
                        new Rectangle(20, 2, W, H),
                        Helpers.NearSF);
                }
                graphics2.DrawString(Text, Font,
                    new SolidBrush(_TextColor),
                    new Rectangle(20, 2, W, H),
                    Helpers.NearSF);
            }
            OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            bitmap.Dispose();
        }

        private void UpdateColors()
        {
            FlatColors colors = Helpers.GetColors(this);
            _BorderColor = colors.Flat;
        }

        private int W;
        private int H;
        private MouseState State;
        private _Options O;
        private bool _Checked;
        private Color _BaseColor = Color.FromArgb(45, 47, 49);
        private Color _TextColor = Color.FromArgb(243, 243, 243);
        private Color _BorderColor = Helpers.FlatColor;

        public delegate void CheckedChangedEventHandler(object sender);

        [Flags]
        public enum _Options
        {
            Style1 = 0,
            Style2 = 1
        }
    }
}

namespace VitNX2.UI.ControlsV2.UiKit
{
    public class FlatColors
    {
        public Color Flat = Helpers.FlatColor;
    }

    public class FormSkin : ContainerControl
    {
        [Category("Color")]
        public Color HeaderColor
        {
            get { return _HeaderColor; }
            set { _HeaderColor = value; }
        }

        [Category("Color")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        [Category("Color")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        [Category("Color")]
        public Color FlatColor
        {
            get { return _FlatColor; }
            set { _FlatColor = value; }
        }

        [Category("Appearance")]
        public bool HeaderMaximize
        {
            get { return _HeaderMaximize; }
            set { _HeaderMaximize = value; }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
            if (e.Button == MouseButtons.Left & new Rectangle(0,
                0,
                Width,
                MoveHeight).Contains(e.Location))
            {
                Cap = true;
                MousePoint = e.Location;
            }
        }

        private void FormSkin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HeaderMaximize && (e.Button == MouseButtons.Left
                & new Rectangle(0, 0,
                Width, MoveHeight).Contains(e.Location)))
            {
                if (FindForm().WindowState == FormWindowState.Normal)
                {
                    FindForm().WindowState = FormWindowState.Maximized;
                    FindForm().Refresh();
                    return;
                }
                if (FindForm().WindowState == FormWindowState.Maximized)
                {
                    FindForm().WindowState = FormWindowState.Normal;
                    FindForm().Refresh();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
            Cap = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
            if (Cap)
                Parent.Location = new Point(MousePosition.X - MousePoint.X,
                    MousePosition.Y - MousePoint.Y);
        }

        protected override void OnCreateControl()
        {
            OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.AllowTransparency = false;
            ParentForm.TransparencyKey = Color.Fuchsia;
            ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
            Dock = DockStyle.Fill;
            Invalidate();
        }

        public FormSkin()
        {
            MouseDoubleClick += FormSkin_MouseDoubleClick;
            SetStyle(ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 12f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            W = Width;
            H = Height;
            Rectangle rect = new Rectangle(0, 0, W, H);
            Rectangle rect2 = new Rectangle(0, 0, W, 50);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(BackColor);
            graphics.FillRectangle(new SolidBrush(_BaseColor), rect);
            graphics.FillRectangle(new SolidBrush(_HeaderColor), rect2);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(243, 243, 243)),
                new Rectangle(13, 16, 4, 18));
            graphics.DrawString(Text,
                Font,
                new SolidBrush(TextColor),
                new Rectangle(26, 15, W, H),
                Helpers.NearSF);
            graphics.DrawRectangle(new Pen(_BorderColor), rect);
            OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            bitmap.Dispose();
        }

        private int W;
        private int H;
        private bool Cap;
        private bool _HeaderMaximize;
        private Point MousePoint = new Point(0, 0);
        private int MoveHeight = 50;
        private Color _HeaderColor = Color.FromArgb(45, 47, 49);
        private Color _BaseColor = Color.FromArgb(60, 70, 73);
        private Color _BorderColor = Color.FromArgb(53, 58, 60);
        private Color _FlatColor = Helpers.FlatColor;
        private Color TextColor = Color.FromArgb(234, 234, 234);
        private Color _HeaderLight = Color.FromArgb(171, 171, 172);
        private Color _BaseLight = Color.FromArgb(196, 199, 200);
        public Color TextLight = Color.FromArgb(45, 47, 49);
    }

    public static class Helpers
    {
        public static GraphicsPath RoundRec(Rectangle Rectangle, int Curve)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            int num = Curve * 2;
            graphicsPath.AddArc(new Rectangle(Rectangle.X, Rectangle.Y,
                num, num),
                -180f, 90f);
            graphicsPath.AddArc(new Rectangle(Rectangle.Width - num + Rectangle.X,
                Rectangle.Y, num, num),
                -90f, 90f);
            graphicsPath.AddArc(new Rectangle(Rectangle.Width - num + Rectangle.X,
                Rectangle.Height - num + Rectangle.Y,
                num, num),
                0f, 90f);
            graphicsPath.AddArc(new Rectangle(Rectangle.X,
                Rectangle.Height - num + Rectangle.Y,
                num, num),
                90f, 90f);
            graphicsPath.AddLine(new Point(Rectangle.X,
                Rectangle.Height - num + Rectangle.Y), new Point(Rectangle.X,
                Curve + Rectangle.Y));
            return graphicsPath;
        }

        public static GraphicsPath RoundRect(float x,
            float y,
            float w,
            float h,
            double r = 0.3,
            bool TL = true,
            bool TR = true,
            bool BR = true,
            bool BL = true)
        {
            float num = Math.Min(w, h) * (float)r;
            float num2 = x + w;
            float num3 = y + h;
            GraphicsPath graphicsPath;
            GraphicsPath result = graphicsPath = new GraphicsPath();
            if (TL)
                graphicsPath.AddArc(x, y,
                    num, num,
                    180f, 90f);
            else
                graphicsPath.AddLine(x, y, x, y);
            if (TR)
                graphicsPath.AddArc(num2 - num, y,
                    num, num,
                    270f, 90f);
            else
                graphicsPath.AddLine(num2, y,
                    num2, y);
            if (BR)
                graphicsPath.AddArc(num2 - num,
                    num3 - num, num,
                    num,
                    0f, 90f);
            else
                graphicsPath.AddLine(num2, num3,
                    num2, num3);
            if (BL)
                graphicsPath.AddArc(x, num3 - num,
                    num, num,
                    90f, 90f);
            else
                graphicsPath.AddLine(x, num3, x, num3);
            graphicsPath.CloseFigure();
            return result;
        }

        public static GraphicsPath DrawArrow(int x, int y, bool flip)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            int num = 12;
            int num2 = 6;
            if (flip)
            {
                graphicsPath.AddLine(x + 1, y,
                    x + num + 1, y);
                graphicsPath.AddLine(x + num, y,
                    x + num2,
                    y + num2 - 1);
            }
            else
            {
                graphicsPath.AddLine(x, y + num2,
                    x + num, y + num2);
                graphicsPath.AddLine(x + num,
                    y + num2,
                    x + num2, y);
            }
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public static FlatColors GetColors(Control control)
        {
            if (control == null)
                throw new ArgumentNullException();
            FlatColors flatColors = new FlatColors();
            while (control != null && control.GetType() !=
                typeof(FormSkin))
                control = control.Parent;
            if (control != null)
            {
                FormSkin formSkin = (FormSkin)control;
                flatColors.Flat = formSkin.FlatColor;
            }
            return flatColors;
        }

        public static Color FlatColor = Color.FromArgb(66, 144, 213);

        public static readonly StringFormat NearSF = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        public static readonly StringFormat CenterSF = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }

    public enum MouseState : byte
    {
        None,
        Over,
        Down,
        Block
    }
}