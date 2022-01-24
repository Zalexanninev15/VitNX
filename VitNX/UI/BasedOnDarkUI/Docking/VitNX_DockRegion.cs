using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VitNX.Config;

namespace VitNX.Docking
{
    [ToolboxItem(false)]
    public class VitNX_DockRegion : Panel
    {
        #region Field Region

        private List<VitNX_DockGroup> _groups;

        private Form _parentForm;
        private VitNX_DockSplitter _splitter;

        #endregion Field Region

        #region Property Region

        public VitNX_DockPanel DockPanel { get; private set; }

        public VitNX_DockArea DockArea { get; private set; }

        public VitNX_DockContent ActiveDocument
        {
            get
            {
                if (DockArea != VitNX_DockArea.Document || _groups.Count == 0)
                    return null;

                return _groups[0].VisibleContent;
            }
        }

        public List<VitNX_DockGroup> Groups
        {
            get
            {
                return _groups.ToList();
            }
        }

        #endregion Property Region

        #region Constructor Region

        public VitNX_DockRegion(VitNX_DockPanel dockPanel, VitNX_DockArea dockArea)
        {
            _groups = new List<VitNX_DockGroup>();

            DockPanel = dockPanel;
            DockArea = dockArea;

            BuildProperties();
        }

        #endregion Constructor Region

        #region Method Region

        internal void AddContent(VitNX_DockContent dockContent)
        {
            AddContent(dockContent, null);
        }

        internal void AddContent(VitNX_DockContent dockContent, VitNX_DockGroup dockGroup)
        {
            // If no existing group is specified then create a new one
            if (dockGroup == null)
            {
                // If this is the document region, then default to first group if it exists
                if (DockArea == VitNX_DockArea.Document && _groups.Count > 0)
                    dockGroup = _groups[0];
                else
                    dockGroup = CreateGroup();
            }

            dockContent.DockRegion = this;
            dockGroup.AddContent(dockContent);

            if (!Visible)
            {
                Visible = true;
                CreateSplitter();
            }

            PositionGroups();
        }

        internal void InsertContent(VitNX_DockContent dockContent, VitNX_DockGroup dockGroup, DockInsertType insertType)
        {
            var order = dockGroup.Order;

            if (insertType == DockInsertType.After)
                order++;

            var newGroup = InsertGroup(order);

            dockContent.DockRegion = this;
            newGroup.AddContent(dockContent);

            if (!Visible)
            {
                Visible = true;
                CreateSplitter();
            }

            PositionGroups();
        }

        internal void RemoveContent(VitNX_DockContent dockContent)
        {
            dockContent.DockRegion = null;

            var group = dockContent.DockGroup;
            group.RemoveContent(dockContent);

            dockContent.DockArea = VitNX_DockArea.None;

            // If that was the final content in the group then remove the group
            if (group.ContentCount == 0)
                RemoveGroup(group);

            // If we just removed the final group, and this isn't the document region, then hide
            if (_groups.Count == 0 && DockArea != VitNX_DockArea.Document)
            {
                Visible = false;
                RemoveSplitter();
            }

            PositionGroups();
        }

        public List<VitNX_DockContent> GetContents()
        {
            var result = new List<VitNX_DockContent>();

            foreach (var group in _groups)
                result.AddRange(group.GetContents());

            return result;
        }

        private VitNX_DockGroup CreateGroup()
        {
            var order = 0;

            if (_groups.Count >= 1)
            {
                order = -1;
                foreach (var group in _groups)
                {
                    if (group.Order >= order)
                        order = group.Order + 1;
                }
            }

            var newGroup = new VitNX_DockGroup(DockPanel, this, order);
            _groups.Add(newGroup);
            Controls.Add(newGroup);

            return newGroup;
        }

        private VitNX_DockGroup InsertGroup(int order)
        {
            foreach (var group in _groups)
            {
                if (group.Order >= order)
                    group.Order++;
            }

            var newGroup = new VitNX_DockGroup(DockPanel, this, order);
            _groups.Add(newGroup);
            Controls.Add(newGroup);

            return newGroup;
        }

        private void RemoveGroup(VitNX_DockGroup group)
        {
            var lastOrder = group.Order;

            _groups.Remove(group);
            Controls.Remove(group);

            foreach (var otherGroup in _groups)
            {
                if (otherGroup.Order > lastOrder)
                    otherGroup.Order--;
            }
        }

        private void PositionGroups()
        {
            DockStyle dockStyle;

            switch (DockArea)
            {
                default:
                case VitNX_DockArea.Document:
                    dockStyle = DockStyle.Fill;
                    break;

                case VitNX_DockArea.Left:
                case VitNX_DockArea.Right:
                    dockStyle = DockStyle.Top;
                    break;

                case VitNX_DockArea.Bottom:
                    dockStyle = DockStyle.Left;
                    break;
            }

            if (_groups.Count == 1)
            {
                _groups[0].Dock = DockStyle.Fill;
                return;
            }

            if (_groups.Count > 1)
            {
                var lastGroup = _groups.OrderByDescending(g => g.Order).First();

                foreach (var group in _groups.OrderByDescending(g => g.Order))
                {
                    group.SendToBack();

                    if (group.Order == lastGroup.Order)
                        group.Dock = DockStyle.Fill;
                    else
                        group.Dock = dockStyle;
                }

                SizeGroups();
            }
        }

        private void SizeGroups()
        {
            if (_groups.Count <= 1)
                return;

            var size = new Size(0, 0);

            switch (DockArea)
            {
                default:
                case VitNX_DockArea.Document:
                    return;

                case VitNX_DockArea.Left:
                case VitNX_DockArea.Right:
                    size = new Size(ClientRectangle.Width, ClientRectangle.Height / _groups.Count);
                    break;

                case VitNX_DockArea.Bottom:
                    size = new Size(ClientRectangle.Width / _groups.Count, ClientRectangle.Height);
                    break;
            }

            foreach (var group in _groups)
                group.Size = size;
        }

        private void BuildProperties()
        {
            MinimumSize = new Size(50, 50);

            switch (DockArea)
            {
                default:
                case VitNX_DockArea.Document:
                    Dock = DockStyle.Fill;
                    Padding = new Padding(0, 1, 0, 0);
                    break;

                case VitNX_DockArea.Left:
                    Dock = DockStyle.Left;
                    Padding = new Padding(0, 0, 1, 0);
                    Visible = false;
                    break;

                case VitNX_DockArea.Right:
                    Dock = DockStyle.Right;
                    Padding = new Padding(1, 0, 0, 0);
                    Visible = false;
                    break;

                case VitNX_DockArea.Bottom:
                    Dock = DockStyle.Bottom;
                    Padding = new Padding(0, 0, 0, 0);
                    Visible = false;
                    break;
            }
        }

        private void CreateSplitter()
        {
            if (_splitter != null && DockPanel.Splitters.Contains(_splitter))
                DockPanel.Splitters.Remove(_splitter);

            switch (DockArea)
            {
                case VitNX_DockArea.Left:
                    _splitter = new VitNX_DockSplitter(DockPanel, this, VitNX_SplitterType.Right);
                    break;

                case VitNX_DockArea.Right:
                    _splitter = new VitNX_DockSplitter(DockPanel, this, VitNX_SplitterType.Left);
                    break;

                case VitNX_DockArea.Bottom:
                    _splitter = new VitNX_DockSplitter(DockPanel, this, VitNX_SplitterType.Top);
                    break;

                default:
                    return;
            }

            DockPanel.Splitters.Add(_splitter);
        }

        private void RemoveSplitter()
        {
            if (DockPanel.Splitters.Contains(_splitter))
                DockPanel.Splitters.Remove(_splitter);
        }

        #endregion Method Region

        #region Event Handler Region

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            _parentForm = FindForm();
            _parentForm.ResizeEnd += ParentForm_ResizeEnd;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            SizeGroups();
        }

        private void ParentForm_ResizeEnd(object sender, EventArgs e)
        {
            if (_splitter != null)
                _splitter.UpdateBounds();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (_splitter != null)
                _splitter.UpdateBounds();
        }

        #endregion Event Handler Region

        #region Paint Region

        public void Redraw()
        {
            Invalidate();

            foreach (var group in _groups)
                group.Redraw();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            if (!Visible)
                return;

            // Fill body
            using (var b = new SolidBrush(Colors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            // Draw border
            using (var p = new Pen(Colors.VitNXBorder))
            {
                // Top border
                if (DockArea == VitNX_DockArea.Document)
                    g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);

                // Left border
                if (DockArea == VitNX_DockArea.Right)
                    g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Left, ClientRectangle.Height);

                // Right border
                if (DockArea == VitNX_DockArea.Left)
                    g.DrawLine(p, ClientRectangle.Right - 1, 0, ClientRectangle.Right - 1, ClientRectangle.Height);
            }
        }

        #endregion Paint Region
    }
}