using System.Collections.Generic;
using System.Drawing;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    public class DockRegionState
    {
        #region Property Region

        public VitNX_DockArea Area { get; set; }

        public Size Size { get; set; }

        public List<DockGroupState> Groups { get; set; }

        #endregion Property Region

        #region Constructor Region

        public DockRegionState()
        {
            Groups = new List<DockGroupState>();
        }

        public DockRegionState(VitNX_DockArea area)
            : this()
        {
            Area = area;
        }

        public DockRegionState(VitNX_DockArea area, Size size)
            : this(area)
        {
            Size = size;
        }

        #endregion Constructor Region
    }
}