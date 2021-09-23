using System.Collections.Generic;
using System.Drawing;

namespace VitNX.Docking
{
    public class DockRegionState
    {
        #region Property Region

        public VitNXDockArea Area { get; set; }

        public Size Size { get; set; }

        public List<DockGroupState> Groups { get; set; }

        #endregion

        #region Constructor Region

        public DockRegionState()
        {
            Groups = new List<DockGroupState>();
        }

        public DockRegionState(VitNXDockArea area)
            : this()
        {
            Area = area;
        }

        public DockRegionState(VitNXDockArea area, Size size)
            : this(area)
        {
            Size = size;
        }

        #endregion
    }
}