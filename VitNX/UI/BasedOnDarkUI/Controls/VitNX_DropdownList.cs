﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VitNX.UI.BasedOnDarkUI.Config;

namespace VitNX.UI.BasedOnDarkUI.Controls
{
    public class VitNX_DropdownList : Control
    {
        #region Event Region

        public event EventHandler SelectedItemChanged;

        #endregion Event Region

        #region Field Region

        private VitNX_ControlState _controlState = VitNX_ControlState.Normal;

        private ObservableCollection<VitNX_DropdownItem> _items = new ObservableCollection<VitNX_DropdownItem>();
        private VitNX_DropdownItem _selectedItem;

        private VitNX_ContextMenu _menu = new VitNX_ContextMenu();
        private bool _menuOpen = false;

        private bool _showBorder = true;

        private int _itemHeight = 22;
        private int _maxHeight = 130;

        private readonly int _iconSize = 16;

        private ToolStripDropDownDirection _dropdownDirection = ToolStripDropDownDirection.Default;

        #endregion Field Region

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<VitNX_DropdownItem> Items
        {
            get { return _items; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DropdownItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                SelectedItemChanged?.Invoke(this, new EventArgs());
            }
        }

        [Category("Appearance")]
        [Description("Determines whether a border is drawn around the control.")]
        [DefaultValue(true)]
        public bool ShowBorder
        {
            get { return _showBorder; }
            set
            {
                _showBorder = value;
                Invalidate();
            }
        }

        protected override Size DefaultSize
        {
            get { return new Size(100, 26); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_ControlState ControlState
        {
            get { return _controlState; }
        }

        [Category("Appearance")]
        [Description("Determines the height of the individual list view items.")]
        [DefaultValue(22)]
        public int ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                _itemHeight = value;
                ResizeMenu();
            }
        }

        [Category("Appearance")]
        [Description("Determines the maximum height of the dropdown panel.")]
        [DefaultValue(130)]
        public int MaxHeight
        {
            get { return _maxHeight; }
            set
            {
                _maxHeight = value;
                ResizeMenu();
            }
        }

        [Category("Behavior")]
        [Description("Determines what location the dropdown list appears.")]
        [DefaultValue(ToolStripDropDownDirection.Default)]
        public ToolStripDropDownDirection DropdownDirection
        {
            get { return _dropdownDirection; }
            set { _dropdownDirection = value; }
        }

        #endregion Property Region

        #region Constructor Region

        public VitNX_DropdownList()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable |
                     ControlStyles.UserMouse, true);

            _menu.AutoSize = false;
            _menu.Closed += Menu_Closed;

            Items.CollectionChanged += Items_CollectionChanged;
            SelectedItemChanged += VitNX_DropdownList_SelectedItemChanged;

            SetControlState(VitNX_ControlState.Normal);
        }

        #endregion Constructor Region

        #region Method Region

        private ToolStripMenuItem GetMenuItem(VitNX_DropdownItem item)
        {
            foreach (ToolStripMenuItem menuItem in _menu.Items)
            {
                if ((VitNX_DropdownItem)menuItem.Tag == item)
                    return menuItem;
            }

            return null;
        }

        private void SetControlState(VitNX_ControlState controlState)
        {
            if (_menuOpen)
                return;

            if (_controlState != controlState)
            {
                _controlState = controlState;
                Invalidate();
            }
        }

        private void ShowMenu()
        {
            if (_menu.Visible)
                return;

            SetControlState(VitNX_ControlState.Pressed);

            _menuOpen = true;

            var pos = new Point(0, ClientRectangle.Bottom);

            if (_dropdownDirection == ToolStripDropDownDirection.AboveLeft || _dropdownDirection == ToolStripDropDownDirection.AboveRight)
                pos.Y = 0;

            _menu.Show(this, pos, _dropdownDirection);

            if (SelectedItem != null)
            {
                var selectedItem = GetMenuItem(SelectedItem);
                selectedItem.Select();
            }
        }

        private void ResizeMenu()
        {
            var width = ClientRectangle.Width;
            var height = (_menu.Items.Count * _itemHeight) + 4;

            if (height > _maxHeight)
                height = _maxHeight;

            // Dirty: Check what the autosized items are
            foreach (ToolStripMenuItem item in _menu.Items)
            {
                item.AutoSize = true;

                if (item.Size.Width > width)
                    width = item.Size.Width;

                item.AutoSize = false;
            }

            // Force the size
            foreach (ToolStripMenuItem item in _menu.Items)
                item.Size = new Size(width - 1, _itemHeight);

            _menu.Size = new Size(width, height);
        }

        #endregion Method Region

        #region Event Handler Region

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (VitNX_DropdownItem item in e.NewItems)
                {
                    var menuItem = new ToolStripMenuItem(item.Text)
                    {
                        Image = item.Icon,
                        AutoSize = false,
                        Height = _itemHeight,
                        Font = Font,
                        Tag = item,
                        TextAlign = ContentAlignment.MiddleLeft
                    };

                    _menu.Items.Add(menuItem);
                    menuItem.Click += Item_Select;

                    if (SelectedItem == null)
                        SelectedItem = item;
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (VitNX_DropdownItem item in e.OldItems)
                {
                    foreach (ToolStripMenuItem menuItem in _menu.Items)
                    {
                        if ((VitNX_DropdownItem)menuItem.Tag == item)
                            _menu.Items.Remove(menuItem);
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _menu.Items.Clear();
                SelectedItem = null;
            }

            ResizeMenu();
        }

        private void Item_Select(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            var dropdownItem = (VitNX_DropdownItem)menuItem.Tag;
            if (_selectedItem != dropdownItem)
                SelectedItem = dropdownItem;
        }

        private void VitNX_DropdownList_SelectedItemChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in _menu.Items)
            {
                if ((VitNX_DropdownItem)item.Tag == SelectedItem)
                {
                    item.BackColor = Colors.VitNXBlueBackground;
                    item.Font = new Font(Font, FontStyle.Bold);
                }
                else
                {
                    item.BackColor = Colors.GreyBackground;
                    item.Font = new Font(Font, FontStyle.Regular);
                }
            }

            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResizeMenu();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(e.Location))
                    SetControlState(VitNX_ControlState.Pressed);
                else
                    SetControlState(VitNX_ControlState.Hover);
            }
            else
            {
                SetControlState(VitNX_ControlState.Hover);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            ShowMenu();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            SetControlState(VitNX_ControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            SetControlState(VitNX_ControlState.Normal);
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetControlState(VitNX_ControlState.Normal);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetControlState(VitNX_ControlState.Normal);
            else
                SetControlState(VitNX_ControlState.Hover);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
                ShowMenu();
        }

        private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            _menuOpen = false;

            if (!ClientRectangle.Contains(MousePosition))
                SetControlState(VitNX_ControlState.Normal);
            else
                SetControlState(VitNX_ControlState.Hover);
        }

        #endregion Event Handler Region

        #region Render Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            // Draw background
            using (var b = new SolidBrush(Colors.MediumBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            // Draw normal state
            if (ControlState == VitNX_ControlState.Normal)
            {
                if (ShowBorder)
                {
                    using (var p = new Pen(Colors.LightBorder, 1))
                    {
                        var modRect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                        g.DrawRectangle(p, modRect);
                    }
                }
            }

            // Draw hover state
            if (ControlState == VitNX_ControlState.Hover)
            {
                using (var b = new SolidBrush(Colors.VitNXBorder))
                {
                    g.FillRectangle(b, ClientRectangle);
                }

                using (var b = new SolidBrush(Colors.VitNXBackground))
                {
                    var arrowRect = new Rectangle(ClientRectangle.Right - DropdownIcons.small_arrow.Width - 8, ClientRectangle.Top, DropdownIcons.small_arrow.Width + 8, ClientRectangle.Height);
                    g.FillRectangle(b, arrowRect);
                }

                using (var p = new Pen(Colors.BlueSelection, 1))
                {
                    var modRect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1 - DropdownIcons.small_arrow.Width - 8, ClientRectangle.Height - 1);
                    g.DrawRectangle(p, modRect);
                }
            }

            // Draw pressed state
            if (ControlState == VitNX_ControlState.Pressed)
            {
                using (var b = new SolidBrush(Colors.VitNXBorder))
                {
                    g.FillRectangle(b, ClientRectangle);
                }

                using (var b = new SolidBrush(Colors.BlueSelection))
                {
                    var arrowRect = new Rectangle(ClientRectangle.Right - DropdownIcons.small_arrow.Width - 8, ClientRectangle.Top, DropdownIcons.small_arrow.Width + 8, ClientRectangle.Height);
                    g.FillRectangle(b, arrowRect);
                }
            }

            // Draw dropdown arrow
            using (var img = DropdownIcons.small_arrow)
            {
                g.DrawImageUnscaled(img, ClientRectangle.Right - img.Width - 4, ClientRectangle.Top + (ClientRectangle.Height / 2) - (img.Height / 2));
            }

            // Draw selected item
            if (SelectedItem != null)
            {
                // Draw Icon
                var hasIcon = SelectedItem.Icon != null;

                if (hasIcon)
                {
                    g.DrawImageUnscaled(SelectedItem.Icon, new Point(ClientRectangle.Left + 5, ClientRectangle.Top + (ClientRectangle.Height / 2) - (_iconSize / 2)));
                }

                // Draw Text
                using (var b = new SolidBrush(Colors.LightText))
                {
                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };

                    var rect = new Rectangle(ClientRectangle.Left + 2, ClientRectangle.Top, ClientRectangle.Width - 16, ClientRectangle.Height);

                    if (hasIcon)
                    {
                        rect.X += _iconSize + 7;
                        rect.Width -= _iconSize + 7;
                    }

                    g.DrawString(SelectedItem.Text, Font, b, rect, stringFormat);
                }
            }
        }

        #endregion Render Region
    }
}