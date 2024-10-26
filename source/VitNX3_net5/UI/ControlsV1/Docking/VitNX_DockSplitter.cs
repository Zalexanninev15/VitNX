﻿using System;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.Forms;

namespace VitNX.UI.ControlsV1.Docking
{
    public class VitNX_DockSplitter
    {
        private Control _parentControl;
        private Control _control;
        private VitNX_SplitterType _splitterType;
        private int _minimum;
        private int _maximum;
        private VitNX_TranslucentForm _overlayForm;

        public Rectangle Bounds { get; set; }
        public Cursor ResizeCursor { get; private set; }

        public VitNX_DockSplitter(Control parentControl,
            Control control,
            VitNX_SplitterType splitterType)
        {
            _parentControl = parentControl;
            _control = control;
            _splitterType = splitterType;
            switch (_splitterType)
            {
                case VitNX_SplitterType.Left:
                case VitNX_SplitterType.Right:
                    ResizeCursor = Cursors.SizeWE;
                    break;

                case VitNX_SplitterType.Top:
                case VitNX_SplitterType.Bottom:
                    ResizeCursor = Cursors.SizeNS;
                    break;
            }
        }

        public void ShowOverlay()
        {
            _overlayForm = new VitNX_TranslucentForm(Color.Black);
            _overlayForm.Visible = true;
            UpdateOverlay(new Point(0, 0));
        }

        public void HideOverlay()
        {
            _overlayForm.Visible = false;
        }

        public void UpdateOverlay(Point difference)
        {
            var bounds = new Rectangle(Bounds.Location,
                Bounds.Size);
            switch (_splitterType)
            {
                case VitNX_SplitterType.Left:
                    var leftX = Math.Max(bounds.Location.X - difference.X,
                        _minimum);
                    if (_maximum != 0 && leftX > _maximum)
                        leftX = _maximum;
                    bounds.Location = new Point(leftX,
                        bounds.Location.Y);
                    break;

                case VitNX_SplitterType.Right:
                    var rightX = Math.Max(bounds.Location.X - difference.X,
                        _minimum);
                    if (_maximum != 0 && rightX > _maximum)
                        rightX = _maximum;
                    bounds.Location = new Point(rightX,
                        bounds.Location.Y);
                    break;

                case VitNX_SplitterType.Top:
                    var topY = Math.Max(bounds.Location.Y - difference.Y,
                        _minimum);
                    if (_maximum != 0 && topY > _maximum)
                        topY = _maximum;
                    bounds.Location = new Point(bounds.Location.X,
                        topY);
                    break;

                case VitNX_SplitterType.Bottom:
                    var bottomY = Math.Max(bounds.Location.Y - difference.Y,
                        _minimum);
                    if (_maximum != 0 && bottomY > _maximum)
                        topY = _maximum;
                    bounds.Location = new Point(bounds.Location.X,
                        bottomY);
                    break;
            }
            _overlayForm.Bounds = bounds;
        }

        public void Move(Point difference)
        {
            switch (_splitterType)
            {
                case VitNX_SplitterType.Left:
                    _control.Width += difference.X;
                    break;

                case VitNX_SplitterType.Right:
                    _control.Width -= difference.X;
                    break;

                case VitNX_SplitterType.Top:
                    _control.Height += difference.Y;
                    break;

                case VitNX_SplitterType.Bottom:
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
                case VitNX_SplitterType.Left:
                    Bounds = new Rectangle(bounds.Left - 2,
                        bounds.Top, 5,
                        bounds.Height);
                    _maximum = bounds.Right - 2 - _control.MinimumSize.Width;
                    break;

                case VitNX_SplitterType.Right:
                    Bounds = new Rectangle(bounds.Right - 2,
                        bounds.Top, 5,
                        bounds.Height);
                    _minimum = bounds.Left - 2 + _control.MinimumSize.Width;
                    break;

                case VitNX_SplitterType.Top:
                    Bounds = new Rectangle(bounds.Left,
                        bounds.Top - 2,
                        bounds.Width, 5);
                    _maximum = bounds.Bottom - 2 - _control.MinimumSize.Height;
                    break;

                case VitNX_SplitterType.Bottom:
                    Bounds = new Rectangle(bounds.Left,
                        bounds.Bottom - 2,
                        bounds.Width, 5);
                    _minimum = bounds.Top - 2 + _control.MinimumSize.Height;
                    break;
            }
        }
    }
}