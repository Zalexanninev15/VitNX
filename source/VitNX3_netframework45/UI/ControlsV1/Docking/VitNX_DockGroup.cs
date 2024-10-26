﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.Config;

namespace VitNX.UI.ControlsV1.Docking
{
    [ToolboxItem(false)]
    public class VitNX_DockGroup : Panel
    {
        private List<VitNX_DockContent> _contents = new List<VitNX_DockContent>();
        private Dictionary<VitNX_DockContent, VitNX_DockTab> _tabs = new Dictionary<VitNX_DockContent, VitNX_DockTab>();
        private VitNX_DockTabArea _tabArea;
        private VitNX_DockTab _dragTab = null;
        public VitNX_DockPanel DockPanel { get; private set; }
        public VitNX_DockRegion DockRegion { get; private set; }
        public VitNX_DockArea DockArea { get; private set; }
        public VitNX_DockContent VisibleContent { get; private set; }
        public int Order { get; set; }

        public int ContentCount
        { get { return _contents.Count; } }

        public VitNX_DockGroup(VitNX_DockPanel dockPanel,
            VitNX_DockRegion dockRegion,
            int order)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            DockPanel = dockPanel;
            DockRegion = dockRegion;
            DockArea = dockRegion.DockArea;
            Order = order;
            _tabArea = new VitNX_DockTabArea(DockArea);
            DockPanel.ActiveContentChanged += DockPanel_ActiveContentChanged;
        }

        public void AddContent(VitNX_DockContent dockContent)
        {
            dockContent.DockGroup = this;
            dockContent.Dock = DockStyle.Fill;
            dockContent.Order = 0;
            if (_contents.Count > 0)
            {
                var order = -1;
                foreach (var otherContent in _contents)
                {
                    if (otherContent.Order >= order)
                        order = otherContent.Order + 1;
                }
                dockContent.Order = order;
            }
            _contents.Add(dockContent);
            Controls.Add(dockContent);
            dockContent.DockTextChanged += DockContent_DockTextChanged;
            _tabs.Add(dockContent, new VitNX_DockTab(dockContent));
            if (VisibleContent == null)
            {
                dockContent.Visible = true;
                VisibleContent = dockContent;
            }
            else
                dockContent.Visible = false;
            var menuItem = new ToolStripMenuItem(dockContent.DockText);
            menuItem.Tag = dockContent;
            menuItem.Click += TabMenuItem_Select;
            menuItem.Image = dockContent.Icon;
            _tabArea.AddMenuItem(menuItem);
            UpdateTabArea();
        }

        public void RemoveContent(VitNX_DockContent dockContent)
        {
            dockContent.DockGroup = null;
            var order = dockContent.Order;
            _contents.Remove(dockContent);
            Controls.Remove(dockContent);
            foreach (var otherContent in _contents)
            {
                if (otherContent.Order > order)
                    otherContent.Order--;
            }
            dockContent.DockTextChanged -= DockContent_DockTextChanged;
            if (_tabs.ContainsKey(dockContent))
                _tabs.Remove(dockContent);
            if (VisibleContent == dockContent)
            {
                VisibleContent = null;
                if (_contents.Count > 0)
                {
                    var newContent = _contents[0];
                    newContent.Visible = true;
                    VisibleContent = newContent;
                }
            }
            var menuItem = _tabArea.GetMenuItem(dockContent);
            menuItem.Click -= TabMenuItem_Select;
            _tabArea.RemoveMenuItem(menuItem);
            UpdateTabArea();
        }

        public List<VitNX_DockContent> GetContents()
        {
            return _contents.OrderBy(c => c.Order).ToList();
        }

        private void UpdateTabArea()
        {
            if (DockArea == VitNX_DockArea.Document)
                _tabArea.Visible = _contents.Count > 0;
            else
                _tabArea.Visible = _contents.Count > 1;
            var size = 0;
            switch (DockArea)
            {
                case VitNX_DockArea.Document:
                    size = _tabArea.Visible ? Constsants.DocumentTabAreaSize : 0;
                    Padding = new Padding(0, size, 0, 0);
                    _tabArea.ClientRectangle = new Rectangle(Padding.Left,
                        0,
                        ClientRectangle.Width - Padding.Horizontal,
                        size);
                    break;

                case VitNX_DockArea.Left:
                case VitNX_DockArea.Right:
                    size = _tabArea.Visible ? Constsants.ToolWindowTabAreaSize : 0;
                    Padding = new Padding(0, 0, 0, size);
                    _tabArea.ClientRectangle = new Rectangle(Padding.Left,
                        ClientRectangle.Bottom - size,
                        ClientRectangle.Width - Padding.Horizontal,
                        size);
                    break;

                case VitNX_DockArea.Bottom:
                    size = _tabArea.Visible ? Constsants.ToolWindowTabAreaSize : 0;
                    Padding = new Padding(1, 0, 0, size);
                    _tabArea.ClientRectangle = new Rectangle(Padding.Left,
                        ClientRectangle.Bottom - size,
                        ClientRectangle.Width - Padding.Horizontal,
                        size);
                    break;
            }
            if (DockArea == VitNX_DockArea.Document)
            {
                var dropdownSize = Constsants.DocumentTabAreaSize;
                _tabArea.DropdownRectangle = new Rectangle(_tabArea.ClientRectangle.Right - dropdownSize, 0,
                    dropdownSize, dropdownSize);
            }
            BuildTabs();
            EnsureVisible();
        }

        private void BuildTabs()
        {
            if (!_tabArea.Visible)
                return;
            SuspendLayout();
            var closeButtonSize = DockIcons.close.Width;
            var totalSize = 0;
            var orderedContent = _contents.OrderBy(c => c.Order);
            foreach (var content in orderedContent)
            {
                int width;
                var tab = _tabs[content];
                using (var g = CreateGraphics())
                    width = tab.CalculateWidth(g, Font);
                if (DockArea == VitNX_DockArea.Document)
                {
                    width += 5;
                    width += closeButtonSize;
                    if (tab.DockContent.Icon != null)
                        width += tab.DockContent.Icon.Width + 5;
                }
                tab.ShowSeparator = true;
                width += 1;
                var y = DockArea == VitNX_DockArea.Document ? 0 : ClientRectangle.Height - Constsants.ToolWindowTabAreaSize;
                var height = DockArea == VitNX_DockArea.Document ? Constsants.DocumentTabAreaSize : Constsants.ToolWindowTabAreaSize;
                var tabRect = new Rectangle(_tabArea.ClientRectangle.Left + totalSize,
                    y,
                    width,
                    height);
                tab.ClientRectangle = tabRect;
                totalSize += width;
            }
            if (DockArea != VitNX_DockArea.Document)
            {
                if (totalSize > _tabArea.ClientRectangle.Width)
                {
                    var difference = totalSize - _tabArea.ClientRectangle.Width;
                    var lastTab = _tabs[orderedContent.Last()];
                    var tabRect = lastTab.ClientRectangle;
                    lastTab.ClientRectangle = new Rectangle(tabRect.Left,
                        tabRect.Top,
                        tabRect.Width - 1,
                        tabRect.Height);
                    lastTab.ShowSeparator = false;
                    var differenceMadeUp = 1;
                    while (differenceMadeUp < difference)
                    {
                        var largest = _tabs.Values.OrderByDescending(tab => tab.ClientRectangle.Width)
                                                                     .First()
                                                                     .ClientRectangle.Width;
                        foreach (var content in orderedContent)
                        {
                            var tab = _tabs[content];
                            if (differenceMadeUp >= difference)
                                break;
                            if (tab.ClientRectangle.Width >= largest)
                            {
                                var rect = tab.ClientRectangle;
                                tab.ClientRectangle = new Rectangle(rect.Left,
                                    rect.Top,
                                    rect.Width - 1,
                                    rect.Height);
                                differenceMadeUp += 1;
                            }
                        }
                    }
                    var xOffset = 0;
                    foreach (var content in orderedContent)
                    {
                        var tab = _tabs[content];
                        var rect = tab.ClientRectangle;
                        tab.ClientRectangle = new Rectangle(_tabArea.ClientRectangle.Left + xOffset,
                            rect.Top,
                            rect.Width,
                            rect.Height);
                        xOffset += rect.Width;
                    }
                }
            }
            if (DockArea == VitNX_DockArea.Document)
            {
                foreach (var content in orderedContent)
                {
                    var tab = _tabs[content];
                    var closeRect = new Rectangle(tab.ClientRectangle.Right - 7 - closeButtonSize - 1,
                                                  tab.ClientRectangle.Top + (tab.ClientRectangle.Height / 2) - (closeButtonSize / 2) - 1,
                                                  closeButtonSize, closeButtonSize);
                    tab.CloseButtonRectangle = closeRect;
                }
            }
            totalSize = 0;
            foreach (var content in orderedContent)
            {
                var tab = _tabs[content];
                totalSize += tab.ClientRectangle.Width;
            }
            _tabArea.TotalTabSize = totalSize;
            ResumeLayout();
            Invalidate();
        }

        public void EnsureVisible()
        {
            if (DockArea != VitNX_DockArea.Document)
                return;
            if (VisibleContent == null)
                return;
            var width = ClientRectangle.Width - Padding.Horizontal - _tabArea.DropdownRectangle.Width;
            var offsetArea = new Rectangle(Padding.Left, 0, width, 0);
            var tab = _tabs[VisibleContent];
            if (tab.ClientRectangle.IsEmpty)
                return;
            if (RectangleToTabArea(tab.ClientRectangle).Left < offsetArea.Left)
                _tabArea.Offset = tab.ClientRectangle.Left;
            else if (RectangleToTabArea(tab.ClientRectangle).Right > offsetArea.Right)
                _tabArea.Offset = tab.ClientRectangle.Right - width;
            if (_tabArea.TotalTabSize < offsetArea.Width)
                _tabArea.Offset = 0;
            if (_tabArea.TotalTabSize > offsetArea.Width)
            {
                var orderedContent = _contents.OrderBy(x => x.Order);
                var lastTab = _tabs[orderedContent.Last()];
                if (lastTab != null)
                {
                    if (RectangleToTabArea(lastTab.ClientRectangle).Right < offsetArea.Right)
                        _tabArea.Offset = lastTab.ClientRectangle.Right - width;
                }
            }
            Invalidate();
        }

        public void SetVisibleContent(VitNX_DockContent content)
        {
            if (!_contents.Contains(content))
                return;
            if (VisibleContent != content)
            {
                VisibleContent = content;
                content.Visible = true;
                foreach (var otherContent in _contents)
                {
                    if (otherContent != content)
                        otherContent.Visible = false;
                }
                Invalidate();
            }
        }

        private Point PointToTabArea(Point point)
        {
            return new Point(point.X - _tabArea.Offset, point.Y);
        }

        private Rectangle RectangleToTabArea(Rectangle rectangle)
        {
            return new Rectangle(PointToTabArea(rectangle.Location), rectangle.Size);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            UpdateTabArea();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_dragTab != null)
            {
                var offsetX = e.Location.X + _tabArea.Offset;
                if (offsetX < _dragTab.ClientRectangle.Left)
                {
                    if (_dragTab.DockContent.Order > 0)
                    {
                        var otherTabs = _tabs.Values.Where(t => t.DockContent.Order ==
                        _dragTab.DockContent.Order - 1).ToList();
                        if (otherTabs.Count == 0)
                            return;
                        var otherTab = otherTabs.First();
                        if (otherTab == null)
                            return;
                        var oldIndex = _dragTab.DockContent.Order;
                        _dragTab.DockContent.Order = oldIndex - 1;
                        otherTab.DockContent.Order = oldIndex;
                        BuildTabs();
                        EnsureVisible();
                        _tabArea.RebuildMenu();
                        return;
                    }
                }
                else if (offsetX > _dragTab.ClientRectangle.Right)
                {
                    var maxOrder = _contents.Count;
                    if (_dragTab.DockContent.Order < maxOrder)
                    {
                        var otherTabs = _tabs.Values.Where(t => t.DockContent.Order ==
                        _dragTab.DockContent.Order + 1).ToList();
                        if (otherTabs.Count == 0)
                            return;
                        var otherTab = otherTabs.First();
                        if (otherTab == null)
                            return;
                        var oldIndex = _dragTab.DockContent.Order;
                        _dragTab.DockContent.Order = oldIndex + 1;
                        otherTab.DockContent.Order = oldIndex;
                        BuildTabs();
                        EnsureVisible();
                        _tabArea.RebuildMenu();
                        return;
                    }
                }
                return;
            }
            if (_tabArea.DropdownRectangle.Contains(e.Location))
            {
                _tabArea.DropdownHot = true;
                foreach (var tab in _tabs.Values)
                    tab.Hot = false;
                Invalidate();
                return;
            }
            _tabArea.DropdownHot = false;
            foreach (var tab in _tabs.Values)
            {
                var rect = RectangleToTabArea(tab.ClientRectangle);
                var hot = rect.Contains(e.Location);
                if (tab.Hot != hot)
                {
                    tab.Hot = hot;
                    Invalidate();
                }
                var closeRect = RectangleToTabArea(tab.CloseButtonRectangle);
                var closeHot = closeRect.Contains(e.Location);
                if (tab.CloseButtonHot != closeHot)
                {
                    tab.CloseButtonHot = closeHot;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_tabArea.DropdownRectangle.Contains(e.Location))
            {
                _tabArea.DropdownHot = true;
                return;
            }
            foreach (var tab in _tabs.Values)
            {
                var rect = RectangleToTabArea(tab.ClientRectangle);
                if (rect.Contains(e.Location))
                {
                    if (e.Button == MouseButtons.Middle)
                    {
                        tab.DockContent.Close();
                        return;
                    }
                    var closeRect = RectangleToTabArea(tab.CloseButtonRectangle);
                    if (closeRect.Contains(e.Location))
                    {
                        _tabArea.ClickedCloseButton = tab;
                        return;
                    }
                    else
                    {
                        DockPanel.ActiveContent = tab.DockContent;
                        EnsureVisible();
                        _dragTab = tab;
                        return;
                    }
                }
            }
            if (VisibleContent != null)
                DockPanel.ActiveContent = VisibleContent;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _dragTab = null;
            if (_tabArea.DropdownRectangle.Contains(e.Location))
            {
                if (_tabArea.DropdownHot)
                    _tabArea.ShowMenu(this, new Point(_tabArea.DropdownRectangle.Left,
                        _tabArea.DropdownRectangle.Bottom - 2));
                return;
            }
            if (_tabArea.ClickedCloseButton == null)
                return;
            var closeRect = RectangleToTabArea(_tabArea.ClickedCloseButton.CloseButtonRectangle);
            if (closeRect.Contains(e.Location))
                _tabArea.ClickedCloseButton.DockContent.Close();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            foreach (var tab in _tabs.Values)
                tab.Hot = false;
            Invalidate();
        }

        private void TabMenuItem_Select(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;
            var content = menuItem.Tag as VitNX_DockContent;
            if (content == null)
                return;
            DockPanel.ActiveContent = content;
        }

        private void DockPanel_ActiveContentChanged(object sender, DockContentEventArgs e)
        {
            if (!_contents.Contains(e.Content))
                return;
            if (e.Content == VisibleContent)
            {
                VisibleContent.Focus();
                return;
            }
            VisibleContent = e.Content;
            foreach (var content in _contents)
                content.Visible = content == VisibleContent;
            VisibleContent.Focus();
            EnsureVisible();
            Invalidate();
        }

        private void DockContent_DockTextChanged(object sender, EventArgs e)
        {
            BuildTabs();
        }

        public void Redraw()
        {
            Invalidate();
            foreach (var content in _contents)
                content.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var b = new SolidBrush(Colors.GreyBackground))
                g.FillRectangle(b, ClientRectangle);
            if (!_tabArea.Visible)
                return;
            using (var b = new SolidBrush(Colors.MediumBackground))
                g.FillRectangle(b, _tabArea.ClientRectangle);
            foreach (var tab in _tabs.Values)
            {
                if (DockArea == VitNX_DockArea.Document)
                    PaintDocumentTab(g, tab);
                else
                    PaintToolWindowTab(g, tab);
            }
            if (DockArea == VitNX_DockArea.Document)
            {
                var isActiveGroup = DockPanel.ActiveGroup == this;
                var divColor = isActiveGroup ? Colors.BlueSelection : Colors.GreySelection;
                using (var b = new SolidBrush(divColor))
                {
                    var divRect = new Rectangle(_tabArea.ClientRectangle.Left,
                        _tabArea.ClientRectangle.Bottom - 2,
                        _tabArea.ClientRectangle.Width, 2);
                    g.FillRectangle(b, divRect);
                }
                var dropdownRect = new Rectangle(_tabArea.DropdownRectangle.Left,
                    _tabArea.DropdownRectangle.Top,
                    _tabArea.DropdownRectangle.Width,
                    _tabArea.DropdownRectangle.Height - 2);
                using (var b = new SolidBrush(Colors.MediumBackground))
                    g.FillRectangle(b, dropdownRect);
                using (var img = DockIcons.arrow)
                    g.DrawImageUnscaled(img, dropdownRect.Left + (dropdownRect.Width / 2) - (img.Width / 2),
                        dropdownRect.Top + (dropdownRect.Height / 2) - (img.Height / 2) + 1);
            }
        }

        private void PaintDocumentTab(Graphics g,
            VitNX_DockTab tab)
        {
            var tabRect = RectangleToTabArea(tab.ClientRectangle);
            var isVisibleTab = VisibleContent == tab.DockContent;
            var isActiveGroup = DockPanel.ActiveGroup == this;
            var bgColor = isVisibleTab ? Colors.BlueSelection : Colors.VitNXBackground;
            if (!isActiveGroup)
                bgColor = isVisibleTab ? Colors.GreySelection : Colors.VitNXBackground;
            if (tab.Hot && !isVisibleTab)
                bgColor = Colors.MediumBackground;
            using (var b = new SolidBrush(bgColor))
                g.FillRectangle(b, tabRect);
            if (tab.ShowSeparator)
            {
                using (var p = new Pen(Colors.VitNXBorder))
                    g.DrawLine(p,
                        tabRect.Right - 1,
                        tabRect.Top,
                        tabRect.Right - 1,
                        tabRect.Bottom);
            }
            var xOffset = 0;
            if (tab.DockContent.Icon != null)
            {
                g.DrawImageUnscaled(tab.DockContent.Icon,
                    tabRect.Left + 5,
                    tabRect.Top + 4);
                xOffset += tab.DockContent.Icon.Width + 2;
            }
            var tabTextFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter
            };
            var textColor = isVisibleTab ? Colors.LightText : Colors.DisabledText;
            using (var b = new SolidBrush(textColor))
            {
                var textRect = new Rectangle(tabRect.Left + 5 + xOffset,
                    tabRect.Top,
                    tabRect.Width - tab.CloseButtonRectangle.Width - 7 - 5 - xOffset,
                    tabRect.Height);
                g.DrawString(tab.DockContent.DockText,
                    Font, b,
                    textRect,
                    tabTextFormat);
            }
            var img = tab.CloseButtonHot ? DockIcons.inactive_close_selected : DockIcons.inactive_close;
            if (isVisibleTab)
            {
                if (isActiveGroup)
                    img = tab.CloseButtonHot ? DockIcons.close_selected : DockIcons.close;
                else
                    img = tab.CloseButtonHot ? DockIcons.close_selected : DockIcons.active_inactive_close;
            }
            var closeRect = RectangleToTabArea(tab.CloseButtonRectangle);
            g.DrawImageUnscaled(img, closeRect.Left, closeRect.Top);
        }

        private void PaintToolWindowTab(Graphics g,
            VitNX_DockTab tab)
        {
            var tabRect = tab.ClientRectangle;
            var isVisibleTab = VisibleContent == tab.DockContent;
            var bgColor = isVisibleTab ? Colors.GreyBackground : Colors.VitNXBackground;
            if (tab.Hot && !isVisibleTab)
                bgColor = Colors.MediumBackground;
            using (var b = new SolidBrush(bgColor))
                g.FillRectangle(b, tabRect);
            if (tab.ShowSeparator)
            {
                using (var p = new Pen(Colors.VitNXBorder))
                    g.DrawLine(p, tabRect.Right - 1,
                        tabRect.Top,
                        tabRect.Right - 1,
                        tabRect.Bottom);
            }
            var tabTextFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter
            };
            var textColor = isVisibleTab ? Colors.BlueHighlight : Colors.DisabledText;
            using (var b = new SolidBrush(textColor))
            {
                var textRect = new Rectangle(tabRect.Left + 5,
                    tabRect.Top,
                    tabRect.Width - 5,
                    tabRect.Height);
                g.DrawString(tab.DockContent.DockText,
                    Font, b,
                    textRect,
                    tabTextFormat);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}