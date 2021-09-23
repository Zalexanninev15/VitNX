using System.Drawing;

namespace VitNX.Docking
{
    internal class DockDropArea
    {
        #region Property Region

        internal VNXDockPanel DockPanel { get; private set; }

        internal Rectangle DropArea { get; private set; }

        internal Rectangle HighlightArea { get; private set; }

        internal VNXDockRegion DockRegion { get; private set; }

        internal VNXDockGroup DockGroup { get; private set; }

        internal DockInsertType InsertType { get; private set; }

        #endregion

        #region Constructor Region

        internal DockDropArea(VNXDockPanel dockPanel, VNXDockRegion region)
        {
            DockPanel = dockPanel;
            DockRegion = region;
            InsertType = DockInsertType.None;

            BuildAreas();
        }

        internal DockDropArea(VNXDockPanel dockPanel, VNXDockGroup group, DockInsertType insertType)
        {
            DockPanel = dockPanel;
            DockGroup = group;
            InsertType = insertType;

            BuildAreas();
        }

        #endregion

        #region Method Region

        internal void BuildAreas()
        {
            if (DockRegion != null)
                BuildRegionAreas();
            else if (DockGroup != null)
                BuildGroupAreas();
        }

        private void BuildRegionAreas()
        {
            switch (DockRegion.DockArea)
            {
                case VNXDockArea.Left:

                    var leftRect = new Rectangle
                    {
                        X = DockPanel.PointToScreen(Point.Empty).X,
                        Y = DockPanel.PointToScreen(Point.Empty).Y,
                        Width = 50,
                        Height = DockPanel.Height
                    };

                    DropArea = leftRect;
                    HighlightArea = leftRect;

                    break;

                case VNXDockArea.Right:

                    var rightRect = new Rectangle
                    {
                        X = DockPanel.PointToScreen(Point.Empty).X + DockPanel.Width - 50,
                        Y = DockPanel.PointToScreen(Point.Empty).Y,
                        Width = 50,
                        Height = DockPanel.Height
                    };

                    DropArea = rightRect;
                    HighlightArea = rightRect;

                    break;

                case VNXDockArea.Bottom:

                    var x = DockPanel.PointToScreen(Point.Empty).X;
                    var width = DockPanel.Width;

                    if (DockPanel.Regions[VNXDockArea.Left].Visible)
                    {
                        x += DockPanel.Regions[VNXDockArea.Left].Width;
                        width -= DockPanel.Regions[VNXDockArea.Left].Width;
                    }

                    if (DockPanel.Regions[VNXDockArea.Right].Visible)
                    {
                        width -= DockPanel.Regions[VNXDockArea.Right].Width;
                    }

                    var bottomRect = new Rectangle
                    {
                        X = x,
                        Y = DockPanel.PointToScreen(Point.Empty).Y + DockPanel.Height - 50,
                        Width = width,
                        Height = 50
                    };

                    DropArea = bottomRect;
                    HighlightArea = bottomRect;

                    break;
            }
        }

        private void BuildGroupAreas()
        {
            switch (InsertType)
            {
                case DockInsertType.None:
                    var dropRect = new Rectangle
                    {
                        X = DockGroup.PointToScreen(Point.Empty).X,
                        Y = DockGroup.PointToScreen(Point.Empty).Y,
                        Width = DockGroup.Width,
                        Height = DockGroup.Height
                    };

                    DropArea = dropRect;
                    HighlightArea = dropRect;

                    break;

                case DockInsertType.Before:
                    var beforeDropWidth = DockGroup.Width;
                    var beforeDropHeight = DockGroup.Height;

                    switch (DockGroup.DockArea)
                    {
                        case VNXDockArea.Left:
                        case VNXDockArea.Right:
                            beforeDropHeight = DockGroup.Height / 4;
                            break;

                        case VNXDockArea.Bottom:
                            beforeDropWidth = DockGroup.Width / 4;
                            break;
                    }

                    var beforeDropRect = new Rectangle
                    {
                        X = DockGroup.PointToScreen(Point.Empty).X,
                        Y = DockGroup.PointToScreen(Point.Empty).Y,
                        Width = beforeDropWidth,
                        Height = beforeDropHeight
                    };

                    DropArea = beforeDropRect;
                    HighlightArea = beforeDropRect;

                    break;

                case DockInsertType.After:
                    var afterDropX = DockGroup.PointToScreen(Point.Empty).X;
                    var afterDropY = DockGroup.PointToScreen(Point.Empty).Y;
                    var afterDropWidth = DockGroup.Width;
                    var afterDropHeight = DockGroup.Height;

                    switch (DockGroup.DockArea)
                    {
                        case VNXDockArea.Left:
                        case VNXDockArea.Right:
                            afterDropHeight = DockGroup.Height / 4;
                            afterDropY = DockGroup.PointToScreen(Point.Empty).Y + DockGroup.Height - afterDropHeight;
                            break;

                        case VNXDockArea.Bottom:
                            afterDropWidth = DockGroup.Width / 4;
                            afterDropX = DockGroup.PointToScreen(Point.Empty).X + DockGroup.Width - afterDropWidth;
                            break;
                    }

                    var afterDropRect = new Rectangle
                    {
                        X = afterDropX,
                        Y = afterDropY,
                        Width = afterDropWidth,
                        Height = afterDropHeight
                    };

                    DropArea = afterDropRect;
                    HighlightArea = afterDropRect;

                    break;
            }
        }

        #endregion
    }
}
