using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Config;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Win32;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    public class VitNX_DockPanel : UserControl
    {
        public event EventHandler<DockContentEventArgs> ActiveContentChanged;

        public event EventHandler<DockContentEventArgs> ContentAdded;

        public event EventHandler<DockContentEventArgs> ContentRemoved;

        private List<VitNX_DockContent> _contents;
        private Dictionary<VitNX_DockArea, VitNX_DockRegion> _regions;
        private VitNX_DockContent _activeContent;
        private bool _switchingContent = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockContent ActiveContent
        {
            get { return _activeContent; }
            set
            {
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
        public VitNX_DockRegion ActiveRegion { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockGroup ActiveGroup { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockContent ActiveDocument
        {
            get { return _regions[VitNX_DockArea.Document].ActiveDocument; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockContentDragFilter DockContentDragFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockResizeFilter DockResizeFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<VitNX_DockSplitter> Splitters { get; private set; }

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
        public Dictionary<VitNX_DockArea, VitNX_DockRegion> Regions
        {
            get { return _regions; }
        }

        public VitNX_DockPanel()
        {
            Splitters = new List<VitNX_DockSplitter>();
            DockContentDragFilter = new DockContentDragFilter(this);
            DockResizeFilter = new DockResizeFilter(this);
            _regions = new Dictionary<VitNX_DockArea, VitNX_DockRegion>();
            _contents = new List<VitNX_DockContent>();
            BackColor = Colors.GreyBackground;
            CreateRegions();
        }

        public void AddContent(VitNX_DockContent dockContent)
        {
            AddContent(dockContent, null);
        }

        public void AddContent(VitNX_DockContent dockContent,
            VitNX_DockGroup dockGroup)
        {
            if (_contents.Contains(dockContent))
                RemoveContent(dockContent);
            dockContent.DockPanel = this;
            _contents.Add(dockContent);
            if (dockGroup != null)
                dockContent.DockArea = dockGroup.DockArea;
            if (dockContent.DockArea == VitNX_DockArea.None)
                dockContent.DockArea = dockContent.DefaultDockArea;
            var region = _regions[dockContent.DockArea];
            region.AddContent(dockContent, dockGroup);
            if (ContentAdded != null)
                ContentAdded(this,
                    new DockContentEventArgs(dockContent));
            dockContent.Select();
        }

        public void InsertContent(VitNX_DockContent dockContent,
            VitNX_DockGroup dockGroup,
            DockInsertType insertType)
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

        public void RemoveContent(VitNX_DockContent dockContent)
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

        public bool ContainsContent(VitNX_DockContent dockContent)
        {
            return _contents.Contains(dockContent);
        }

        public List<VitNX_DockContent> GetDocuments()
        {
            return _regions[VitNX_DockArea.Document].GetContents();
        }

        private void CreateRegions()
        {
            var documentRegion = new VitNX_DockRegion(this,
                VitNX_DockArea.Document);
            _regions.Add(VitNX_DockArea.Document, documentRegion);
            var leftRegion = new VitNX_DockRegion(this,
                VitNX_DockArea.Left);
            _regions.Add(VitNX_DockArea.Left, leftRegion);
            var rightRegion = new VitNX_DockRegion(this,
                VitNX_DockArea.Right);
            _regions.Add(VitNX_DockArea.Right, rightRegion);
            var bottomRegion = new VitNX_DockRegion(this,
                VitNX_DockArea.Bottom);
            _regions.Add(VitNX_DockArea.Bottom, bottomRegion);
            Controls.Add(documentRegion);
            Controls.Add(bottomRegion);
            Controls.Add(leftRegion);
            Controls.Add(rightRegion);
            documentRegion.TabIndex = 0;
            rightRegion.TabIndex = 1;
            bottomRegion.TabIndex = 2;
            leftRegion.TabIndex = 3;
        }

        public void DragContent(VitNX_DockContent content)
        {
            DockContentDragFilter.StartDrag(content);
        }

        public DockPanelState GetDockPanelState()
        {
            var state = new DockPanelState();
            state.Regions.Add(new DockRegionState(VitNX_DockArea.Document));
            state.Regions.Add(new DockRegionState(VitNX_DockArea.Left,
                _regions[VitNX_DockArea.Left].Size));
            state.Regions.Add(new DockRegionState(VitNX_DockArea.Right,
                _regions[VitNX_DockArea.Right].Size));
            state.Regions.Add(new DockRegionState(VitNX_DockArea.Bottom,
                _regions[VitNX_DockArea.Bottom].Size));
            var _groupStates = new Dictionary<VitNX_DockGroup, DockGroupState>();
            var orderedContent = _contents.OrderBy(c => c.Order);
            foreach (var content in orderedContent)
            {
                foreach (var region in state.Regions)
                {
                    if (region.Area == content.DockArea)
                    {
                        DockGroupState groupState;
                        if (_groupStates.ContainsKey(content.DockGroup))
                            groupState = _groupStates[content.DockGroup];
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

        public void RestoreDockPanelState(DockPanelState state,
            Func<string, VitNX_DockContent> getContentBySerializationKey)
        {
            foreach (var region in state.Regions)
            {
                switch (region.Area)
                {
                    case VitNX_DockArea.Left:
                        _regions[VitNX_DockArea.Left].Size = region.Size;
                        break;

                    case VitNX_DockArea.Right:
                        _regions[VitNX_DockArea.Right].Size = region.Size;
                        break;

                    case VitNX_DockArea.Bottom:
                        _regions[VitNX_DockArea.Bottom].Size = region.Size;
                        break;
                }
                foreach (var group in region.Groups)
                {
                    VitNX_DockContent previousContent = null;
                    VitNX_DockContent visibleContent = null;
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
    }
}