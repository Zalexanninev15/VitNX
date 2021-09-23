using VitNX.Config;
using VitNX.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace VitNX.Docking
{
    public class VNXDockPanel : UserControl
    {
        #region Event Region

        public event EventHandler<DockContentEventArgs> ActiveContentChanged;
        public event EventHandler<DockContentEventArgs> ContentAdded;
        public event EventHandler<DockContentEventArgs> ContentRemoved;

        #endregion

        #region Field Region

        private List<VNXDockContent> _contents;
        private Dictionary<VNXDockArea, VNXDockRegion> _regions;

        private VNXDockContent _activeContent;
        private bool _switchingContent = false;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VNXDockContent ActiveContent
        {
            get { return _activeContent; }
            set
            {
                // Don't let content visibility changes re-trigger event
                if (_switchingContent)
                    return;

                _switchingContent = true;

                _activeContent = value;

                ActiveGroup = _activeContent.DockGroup;
                ActiveRegion = ActiveGroup.DockRegion;

                foreach (var region in _regions.Values)
                    region.Redraw();

                if (ActiveContentChanged != null)
                    ActiveContentChanged(this, new DockContentEventArgs(_activeContent));

                _switchingContent = false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VNXDockRegion ActiveRegion { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VNXDockGroup ActiveGroup { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VNXDockContent ActiveDocument
        {
            get
            {
                return _regions[VNXDockArea.Document].ActiveDocument;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockContentDragFilter DockContentDragFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockResizeFilter DockResizeFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<VNXDockSplitter> Splitters { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MouseButtons MouseButtonState
        {
            get
            {
                var buttonState = MouseButtons;
                return buttonState;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<VNXDockArea, VNXDockRegion> Regions
        {
            get
            {
                return _regions;
            }
        }

        #endregion

        #region Constructor Region

        public VNXDockPanel()
        {
            Splitters = new List<VNXDockSplitter>();
            DockContentDragFilter = new DockContentDragFilter(this);
            DockResizeFilter = new DockResizeFilter(this);

            _regions = new Dictionary<VNXDockArea, VNXDockRegion>();
            _contents = new List<VNXDockContent>();

            BackColor = Colors.GreyBackground;

            CreateRegions();
        }

        #endregion

        #region Method Region

        public void AddContent(VNXDockContent dockContent)
        {
            AddContent(dockContent, null);
        }

        public void AddContent(VNXDockContent dockContent, VNXDockGroup dockGroup)
        {
            if (_contents.Contains(dockContent))
                RemoveContent(dockContent);

            dockContent.DockPanel = this;
            _contents.Add(dockContent);

            if (dockGroup != null)
                dockContent.DockArea = dockGroup.DockArea;

            if (dockContent.DockArea == VNXDockArea.None)
                dockContent.DockArea = dockContent.DefaultDockArea;

            var region = _regions[dockContent.DockArea];
            region.AddContent(dockContent, dockGroup);

            if (ContentAdded != null)
                ContentAdded(this, new DockContentEventArgs(dockContent));

            dockContent.Select();
        }

        public void InsertContent(VNXDockContent dockContent, VNXDockGroup dockGroup, DockInsertType insertType)
        {
            if (_contents.Contains(dockContent))
                RemoveContent(dockContent);

            dockContent.DockPanel = this;
            _contents.Add(dockContent);

            dockContent.DockArea = dockGroup.DockArea;

            var region = _regions[dockGroup.DockArea];
            region.InsertContent(dockContent, dockGroup, insertType);

            if (ContentAdded != null)
                ContentAdded(this, new DockContentEventArgs(dockContent));

            dockContent.Select();
        }

        public void RemoveContent(VNXDockContent dockContent)
        {
            if (!_contents.Contains(dockContent))
                return;

            dockContent.DockPanel = null;
            _contents.Remove(dockContent);

            var region = _regions[dockContent.DockArea];
            region.RemoveContent(dockContent);

            if (ContentRemoved != null)
                ContentRemoved(this, new DockContentEventArgs(dockContent));
        }

        public bool ContainsContent(VNXDockContent dockContent)
        {
            return _contents.Contains(dockContent);
        }

        public List<VNXDockContent> GetDocuments()
        {
            return _regions[VNXDockArea.Document].GetContents();
        }

        private void CreateRegions()
        {
            var documentRegion = new VNXDockRegion(this, VNXDockArea.Document);
            _regions.Add(VNXDockArea.Document, documentRegion);

            var leftRegion = new VNXDockRegion(this, VNXDockArea.Left);
            _regions.Add(VNXDockArea.Left, leftRegion);

            var rightRegion = new VNXDockRegion(this, VNXDockArea.Right);
            _regions.Add(VNXDockArea.Right, rightRegion);

            var bottomRegion = new VNXDockRegion(this, VNXDockArea.Bottom);
            _regions.Add(VNXDockArea.Bottom, bottomRegion);

            // Add the regions in this order to force the bottom region to be positioned
            // between the left and right regions properly.
            Controls.Add(documentRegion);
            Controls.Add(bottomRegion);
            Controls.Add(leftRegion);
            Controls.Add(rightRegion);

            // Create tab index for intuitive tabbing order
            documentRegion.TabIndex = 0;
            rightRegion.TabIndex = 1;
            bottomRegion.TabIndex = 2;
            leftRegion.TabIndex = 3;
        }

        public void DragContent(VNXDockContent content)
        {
            DockContentDragFilter.StartDrag(content);
        }

        #endregion

        #region Serialization Region

        public DockPanelState GetDockPanelState()
        {
            var state = new DockPanelState();

            state.Regions.Add(new DockRegionState(VNXDockArea.Document));
            state.Regions.Add(new DockRegionState(VNXDockArea.Left, _regions[VNXDockArea.Left].Size));
            state.Regions.Add(new DockRegionState(VNXDockArea.Right, _regions[VNXDockArea.Right].Size));
            state.Regions.Add(new DockRegionState(VNXDockArea.Bottom, _regions[VNXDockArea.Bottom].Size));

            var _groupStates = new Dictionary<VNXDockGroup, DockGroupState>();

            var orderedContent = _contents.OrderBy(c => c.Order);
            foreach (var content in orderedContent)
            {
                foreach (var region in state.Regions)
                {
                    if (region.Area == content.DockArea)
                    {
                        DockGroupState groupState;

                        if (_groupStates.ContainsKey(content.DockGroup))
                        {
                            groupState = _groupStates[content.DockGroup];
                        }
                        else
                        {
                            groupState = new DockGroupState();
                            region.Groups.Add(groupState);
                            _groupStates.Add(content.DockGroup, groupState);
                        }

                        groupState.Contents.Add(content.SerializationKey);

                        groupState.VisibleContent = content.DockGroup.VisibleContent.SerializationKey;
                    }
                }
            }

            return state;
        }

        public void RestoreDockPanelState(DockPanelState state, Func<string, VNXDockContent> getContentBySerializationKey)
        {
            foreach (var region in state.Regions)
            {
                switch (region.Area)
                {
                    case VNXDockArea.Left:
                        _regions[VNXDockArea.Left].Size = region.Size;
                        break;
                    case VNXDockArea.Right:
                        _regions[VNXDockArea.Right].Size = region.Size;
                        break;
                    case VNXDockArea.Bottom:
                        _regions[VNXDockArea.Bottom].Size = region.Size;
                        break;
                }

                foreach (var group in region.Groups)
                {
                    VNXDockContent previousContent = null;
                    VNXDockContent visibleContent = null;

                    foreach (var contentKey in group.Contents)
                    {
                        var content = getContentBySerializationKey(contentKey);

                        if (content == null)
                            continue;

                        content.DockArea = region.Area;

                        if (previousContent == null)
                            AddContent(content);
                        else
                            AddContent(content, previousContent.DockGroup);

                        previousContent = content;

                        if (group.VisibleContent == contentKey)
                            visibleContent = content;
                    }

                    if (visibleContent != null)
                        visibleContent.Select();
                }
            }
        }

        #endregion
    }
}
    