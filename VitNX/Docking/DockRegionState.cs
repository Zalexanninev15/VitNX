using System.Collections.Generic;
using System.Drawing;

namespace VitNX.Docking
{
    public class DockRegionState
    {
        #region Property Region

        public VNXDockArea Area { get; set; }

        public Size Size { get; set; }

        public List<DockGroupState> Groups { get; set; }

        #endregion

        #region Constructor Region

        public DockRegionState()
        {
            Groups = new List<DockGroupState>();
        }

        public DockRegionState(VNXDockArea area)
            : this()
        {
            Area = area;
        }

        public DockRegionState(VNXDockArea area, Size size)
            : this(area)
        {
            Size = size;
        }

        #endregion
    }
}
