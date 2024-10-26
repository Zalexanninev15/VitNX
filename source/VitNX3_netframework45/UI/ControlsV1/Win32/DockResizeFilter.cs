﻿using System;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.Docking;

using VitNX3.Functions.Win32;

namespace VitNX.UI.ControlsV1.Win32
{
    public class DockResizeFilter : IMessageFilter
    {
        private VitNX_DockPanel _dockPanel;
        private Timer _dragTimer;
        private bool _isDragging;
        private Point _initialContact;
        private VitNX_DockSplitter _activeSplitter;

        public DockResizeFilter(VitNX_DockPanel dockPanel)
        {
            _dockPanel = dockPanel;
            _dragTimer = new Timer();
            _dragTimer.Interval = 1;
            _dragTimer.Tick += DragTimer_Tick;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!(m.Msg == (int)Enums.WINDOW_MESSAGE.MOUSE_MOVE ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_DOWN ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_UP ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_DBL_CLK ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.R_BUTTON_DOWN ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.R_BUTTON_UP ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.R_BUTTON_DBL_CLK))
                return false;
            if (m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_UP)
            {
                if (_isDragging)
                {
                    StopDrag();
                    return true;
                }
            }
            if (m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_UP &&
                !_isDragging)
                return false;
            if (_isDragging)
                Cursor.Current = _activeSplitter.ResizeCursor;
            if (m.Msg == (int)Enums.WINDOW_MESSAGE.MOUSE_MOVE &&
                !_isDragging &&
                _dockPanel.MouseButtonState !=
                MouseButtons.None)
                return false;
            var control = Control.FromHandle(m.HWnd);
            if (control == null)
                return false;
            if (!(control == _dockPanel || _dockPanel.Contains(control)))
                return false;
            CheckCursor();
            if (m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_DOWN)
            {
                var hotSplitter = HotSplitter();
                if (hotSplitter != null)
                {
                    StartDrag(hotSplitter);
                    return true;
                }
            }
            if (HotSplitter() != null)
                return true;
            if (_isDragging)
                return true;
            return false;
        }

        private void DragTimer_Tick(object sender, EventArgs e)
        {
            if (_dockPanel.MouseButtonState != MouseButtons.Left)
            {
                StopDrag();
                return;
            }
            var difference = new Point(_initialContact.X - Cursor.Position.X
                , _initialContact.Y - Cursor.Position.Y);
            _activeSplitter.UpdateOverlay(difference);
        }

        private void StartDrag(VitNX_DockSplitter splitter)
        {
            _activeSplitter = splitter;
            Cursor.Current = _activeSplitter.ResizeCursor;
            _initialContact = Cursor.Position;
            _isDragging = true;
            _activeSplitter.ShowOverlay();
            _dragTimer.Start();
        }

        private void StopDrag()
        {
            _dragTimer.Stop();
            _activeSplitter.HideOverlay();
            var difference = new Point(_initialContact.X - Cursor.Position.X,
                _initialContact.Y - Cursor.Position.Y);
            _activeSplitter.Move(difference);
            _isDragging = false;
        }

        private VitNX_DockSplitter HotSplitter()
        {
            foreach (var splitter in _dockPanel.Splitters)
            {
                if (splitter.Bounds.Contains(Cursor.Position))
                    return splitter;
            }
            return null;
        }

        private void CheckCursor()
        {
            if (_isDragging)
                return;
            var hotSplitter = HotSplitter();
            if (hotSplitter != null)
                Cursor.Current = hotSplitter.ResizeCursor;
        }

        private void ResetCursor()
        {
            Cursor.Current = Cursors.Default;
            CheckCursor();
        }
    }
}