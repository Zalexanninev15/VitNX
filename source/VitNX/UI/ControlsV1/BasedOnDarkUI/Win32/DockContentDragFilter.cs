﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Config;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Docking;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Forms;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Win32
{
    public class DockContentDragFilter : IMessageFilter
    {
        #region Field Region

        private VitNX_DockPanel _dockPanel;

        private VitNX_DockContent _dragContent;

        private VitNX_TranslucentForm _highlightForm;

        private bool _isDragging = false;
        private VitNX_DockRegion _targetRegion;
        private VitNX_DockGroup _targetGroup;
        private DockInsertType _insertType = DockInsertType.None;

        private Dictionary<VitNX_DockRegion, DockDropArea> _regionDropAreas = new Dictionary<VitNX_DockRegion, DockDropArea>();
        private Dictionary<VitNX_DockGroup, DockDropCollection> _groupDropAreas = new Dictionary<VitNX_DockGroup, DockDropCollection>();

        #endregion Field Region

        #region Constructor Region

        public DockContentDragFilter(VitNX_DockPanel dockPanel)
        {
            _dockPanel = dockPanel;

            _highlightForm = new VitNX_TranslucentForm(Colors.BlueSelection);
        }

        #endregion Constructor Region

        #region IMessageFilter Region

        public bool PreFilterMessage(ref Message m)
        {
            // Exit out early if we're not dragging any content
            if (!_isDragging)
                return false;

            // We only care about mouse events
            if (!(m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.MOUSEMOVE ||
                  m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.LBUTTONDOWN || m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.LBUTTONUP || m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.LBUTTONDBLCLK ||
                  m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.RBUTTONDOWN || m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.RBUTTONUP || m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.RBUTTONDBLCLK))
                return false;

            // Move content
            if (m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.MOUSEMOVE)
            {
                HandleDrag();
                return false;
            }

            // Drop content
            if (m.Msg == (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.LBUTTONUP)
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
                            _dockPanel.AddContent(_dragContent, _targetGroup);
                            break;

                        case DockInsertType.Before:
                        case DockInsertType.After:
                            _dockPanel.InsertContent(_dragContent, _targetGroup, _insertType);
                            break;
                    }
                }

                StopDrag();
                return false;
            }

            return true;
        }

        #endregion IMessageFilter Region

        #region Method Region

        public void StartDrag(VitNX_DockContent content)
        {
            _regionDropAreas = new Dictionary<VitNX_DockRegion, DockDropArea>();
            _groupDropAreas = new Dictionary<VitNX_DockGroup, DockDropCollection>();

            // Add all regions and groups to the drop collections
            foreach (var region in _dockPanel.Regions.Values)
            {
                if (region.DockArea == VitNX_DockArea.Document)
                    continue;

                // If the region is visible then build drop areas for the groups.
                if (region.Visible)
                {
                    foreach (var group in region.Groups)
                    {
                        var collection = new DockDropCollection(_dockPanel, group);
                        _groupDropAreas.Add(group, collection);
                    }
                }
                // If the region is NOT visible then build the drop area for the region itself.
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

            _highlightForm.Size = new Size(rect.Width, rect.Height);
            _highlightForm.Location = new Point(rect.X, rect.Y);

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

            // Check all region drop areas
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

            // Check all group drop areas
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

                // If we're hovering over the group itself, only allow inserting before/after if multiple content is tabbed.
                if (!sameGroup || groupHasOtherContent)
                {
                    var skipBefore = false;
                    var skipAfter = false;

                    // Inserting before/after other content might cause the content to be dropped on to its own location.
                    // Check if the group above/below the hovered group contains our drag content.
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

                // Don't allow content to be dragged on to itself
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

            // Not hovering over anything - hide the highlight
            if (_highlightForm.Visible)
                _highlightForm.Hide();

            // Show we can't drag here
            Cursor.Current = Cursors.No;
        }

        #endregion Method Region
    }
}