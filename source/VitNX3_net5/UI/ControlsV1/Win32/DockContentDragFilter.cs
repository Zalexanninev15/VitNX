using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.Config;
using VitNX.UI.ControlsV1.Docking;
using VitNX.UI.ControlsV1.Forms;

using VitNX3.Functions.Win32;

namespace VitNX.UI.ControlsV1.Win32
{
    public class DockContentDragFilter : IMessageFilter
    {
        private VitNX_DockPanel _dockPanel;
        private VitNX_DockContent _dragContent;
        private VitNX_TranslucentForm _highlightForm;
        private bool _isDragging = false;
        private VitNX_DockRegion _targetRegion;
        private VitNX_DockGroup _targetGroup;
        private DockInsertType _insertType = DockInsertType.None;
        private Dictionary<VitNX_DockRegion, DockDropArea> _regionDropAreas = new Dictionary<VitNX_DockRegion, DockDropArea>();
        private Dictionary<VitNX_DockGroup, DockDropCollection> _groupDropAreas = new Dictionary<VitNX_DockGroup, DockDropCollection>();

        public DockContentDragFilter(VitNX_DockPanel dockPanel)
        {
            _dockPanel = dockPanel;
            _highlightForm = new VitNX_TranslucentForm(Colors.BlueSelection);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!_isDragging)
                return false;
            if (!(m.Msg == (int)Enums.WINDOW_MESSAGE.MOUSE_MOVE ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_DOWN ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_UP ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_DBL_CLK ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.R_BUTTON_DOWN ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.R_BUTTON_UP ||
                  m.Msg == (int)Enums.WINDOW_MESSAGE.R_BUTTON_DBL_CLK))
                return false;
            if (m.Msg == (int)Enums.WINDOW_MESSAGE.MOUSE_MOVE)
            {
                HandleDrag();
                return false;
            }
            if (m.Msg == (int)Enums.WINDOW_MESSAGE.L_BUTTON_UP)
            {
                if (_targetRegion != null)
                {
                    _dockPanel.RemoveContent(_dragContent);
                    _dragContent.DockArea = _targetRegion.DockArea;
                    _dockPanel.AddContent(_dragContent);
                }
                else if (_targetGroup != null)
                {
                    _dockPanel.RemoveContent(_dragContent);
                    switch (_insertType)
                    {
                        case DockInsertType.None:
                            _dockPanel.AddContent(_dragContent,
                                _targetGroup);
                            break;

                        case DockInsertType.Before:
                        case DockInsertType.After:
                            _dockPanel.InsertContent(_dragContent,
                                _targetGroup,
                                _insertType);
                            break;
                    }
                }
                StopDrag();
                return false;
            }
            return true;
        }

        public void StartDrag(VitNX_DockContent content)
        {
            _regionDropAreas = new Dictionary<VitNX_DockRegion,
                DockDropArea>();
            _groupDropAreas = new Dictionary<VitNX_DockGroup,
                DockDropCollection>();
            foreach (var region in _dockPanel.Regions.Values)
            {
                if (region.DockArea == VitNX_DockArea.Document)
                    continue;
                if (region.Visible)
                {
                    foreach (var group in region.Groups)
                    {
                        var collection = new DockDropCollection(_dockPanel, group);
                        _groupDropAreas.Add(group, collection);
                    }
                }
                else
                {
                    var area = new DockDropArea(_dockPanel, region);
                    _regionDropAreas.Add(region, area);
                }
            }
            _dragContent = content;
            _isDragging = true;
        }

        private void StopDrag()
        {
            Cursor.Current = Cursors.Default;
            _highlightForm.Hide();
            _dragContent = null;
            _isDragging = false;
        }

        private void UpdateHighlightForm(Rectangle rect)
        {
            Cursor.Current = Cursors.SizeAll;
            _highlightForm.SuspendLayout();
            _highlightForm.Size = new Size(rect.Width,
                rect.Height);
            _highlightForm.Location = new Point(rect.X,
                rect.Y);
            _highlightForm.ResumeLayout();
            if (!_highlightForm.Visible)
            {
                _highlightForm.Show();
                _highlightForm.BringToFront();
            }
        }

        private void HandleDrag()
        {
            var location = Cursor.Position;
            _insertType = DockInsertType.None;
            _targetRegion = null;
            _targetGroup = null;
            foreach (var area in _regionDropAreas.Values)
            {
                if (area.DropArea.Contains(location))
                {
                    _insertType = DockInsertType.None;
                    _targetRegion = area.DockRegion;
                    UpdateHighlightForm(area.HighlightArea);
                    return;
                }
            }
            foreach (var collection in _groupDropAreas.Values)
            {
                var sameRegion = false;
                var sameGroup = false;
                var groupHasOtherContent = false;
                if (collection.DropArea.DockGroup == _dragContent.DockGroup)
                    sameGroup = true;
                if (collection.DropArea.DockGroup.DockRegion == _dragContent.DockRegion)
                    sameRegion = true;
                if (_dragContent.DockGroup.ContentCount > 1)
                    groupHasOtherContent = true;
                if (!sameGroup || groupHasOtherContent)
                {
                    var skipBefore = false;
                    var skipAfter = false;
                    if (sameRegion && !groupHasOtherContent)
                    {
                        if (collection.InsertBeforeArea.DockGroup.Order == _dragContent.DockGroup.Order + 1)
                            skipBefore = true;
                        if (collection.InsertAfterArea.DockGroup.Order == _dragContent.DockGroup.Order - 1)
                            skipAfter = true;
                    }
                    if (!skipBefore)
                    {
                        if (collection.InsertBeforeArea.DropArea.Contains(location))
                        {
                            _insertType = DockInsertType.Before;
                            _targetGroup = collection.InsertBeforeArea.DockGroup;
                            UpdateHighlightForm(collection.InsertBeforeArea.HighlightArea);
                            return;
                        }
                    }
                    if (!skipAfter)
                    {
                        if (collection.InsertAfterArea.DropArea.Contains(location))
                        {
                            _insertType = DockInsertType.After;
                            _targetGroup = collection.InsertAfterArea.DockGroup;
                            UpdateHighlightForm(collection.InsertAfterArea.HighlightArea);
                            return;
                        }
                    }
                }
                if (!sameGroup)
                {
                    if (collection.DropArea.DropArea.Contains(location))
                    {
                        _insertType = DockInsertType.None;
                        _targetGroup = collection.DropArea.DockGroup;
                        UpdateHighlightForm(collection.DropArea.HighlightArea);
                        return;
                    }
                }
            }
            if (_highlightForm.Visible)
                _highlightForm.Hide();
            Cursor.Current = Cursors.No;
        }
    }
}