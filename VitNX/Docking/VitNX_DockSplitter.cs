using VitNX.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Docking
{
    public class VitNXDockSplitter
    {
        #region Field Region

        private Control _parentControl;
        private Control _control;

        private VitNXSplitterType _splitterType;

        private int _minimum;
        private int _maximum;
        private VitNXTranslucentForm _overlayForm;

        #endregion

        #region Property Region

        public Rectangle Bounds { get; set; }

        public Cursor ResizeCursor { get; private set; }

        #endregion

        #region Constructor Region

        public VitNXDockSplitter(Control parentControl, Control control, VitNXSplitterType splitterType)
        {
            _parentControl = parentControl;
            _control = control;
            _splitterType = splitterType;

            switch (_splitterType)
            {
                case VitNXSplitterType.Left:
                case VitNXSplitterType.Right:
                    ResizeCursor = Cursors.SizeWE;
                    break;
                case VitNXSplitterType.Top:
                case VitNXSplitterType.Bottom:
                    ResizeCursor = Cursors.SizeNS;
                    break;
            }
        }

        #endregion

        #region Method Region

        public void ShowOverlay()
        {
            _overlayForm = new VitNXTranslucentForm(Color.Black);
            _overlayForm.Visible = true;

            UpdateOverlay(new Point(0, 0));
        }

        public void HideOverlay()
        {
            _overlayForm.Visible = false;
        }

        public void UpdateOverlay(Point difference)
        {
            var bounds = new Rectangle(Bounds.Location, Bounds.Size);

            switch (_splitterType)
            {
                case VitNXSplitterType.Left:
                    var leftX = Math.Max(bounds.Location.X - difference.X, _minimum);

                    if (_maximum != 0 && leftX > _maximum)
                        leftX = _maximum;

                    bounds.Location = new Point(leftX, bounds.Location.Y);
                    break;
                case VitNXSplitterType.Right:
                    var rightX = Math.Max(bounds.Location.X - difference.X, _minimum);

                    if (_maximum != 0 && rightX > _maximum)
                        rightX = _maximum;

                    bounds.Location = new Point(rightX, bounds.Location.Y);
                    break;
                case VitNXSplitterType.Top:
                    var topY = Math.Max(bounds.Location.Y - difference.Y, _minimum);

                    if (_maximum != 0 && topY > _maximum)
                        topY = _maximum;

                    bounds.Location = new Point(bounds.Location.X, topY);
                    break;
                case VitNXSplitterType.Bottom:
                    var bottomY = Math.Max(bounds.Location.Y - difference.Y, _minimum);

                    if (_maximum != 0 && bottomY > _maximum)
                        topY = _maximum;

                    bounds.Location = new Point(bounds.Location.X, bottomY);
                    break;
            }

            _overlayForm.Bounds = bounds;
        }

        public void Move(Point difference)
        {
            switch (_splitterType)
            {
                case VitNXSplitterType.Left:
                    _control.Width += difference.X;
                    break;
                case VitNXSplitterType.Right:
                    _control.Width -= difference.X;
                    break;
                case VitNXSplitterType.Top:
                    _control.Height += difference.Y;
                    break;
                case VitNXSplitterType.Bottom:
                    _control.Height -= difference.Y;
                    break;
            }

            UpdateBounds();
        }

        public void UpdateBounds()
        {
            var bounds = _parentControl.RectangleToScreen(_control.Bounds);

            switch (_splitterType)
            {
                case VitNXSplitterType.Left:
                    Bounds = new Rectangle(bounds.Left - 2, bounds.Top, 5, bounds.Height);
                    _maximum = bounds.Right - 2 - _control.MinimumSize.Width;
                    break;
                case VitNXSplitterType.Right:
                    Bounds = new Rectangle(bounds.Right - 2, bounds.Top, 5, bounds.Height);
                    _minimum = bounds.Left - 2 + _control.MinimumSize.Width;
                    break;
                case VitNXSplitterType.Top:
                    Bounds = new Rectangle(bounds.Left, bounds.Top - 2, bounds.Width, 5);
                    _maximum = bounds.Bottom - 2 - _control.MinimumSize.Height;
                    break;
                case VitNXSplitterType.Bottom:
                    Bounds = new Rectangle(bounds.Left, bounds.Bottom - 2, bounds.Width, 5);
                    _minimum = bounds.Top - 2 + _control.MinimumSize.Height;
                    break;
            }
        }

        #endregion
    }
}
