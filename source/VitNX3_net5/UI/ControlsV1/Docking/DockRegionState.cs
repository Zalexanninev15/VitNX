using System.Collections.Generic;
using System.Drawing;

namespace VitNX.UI.ControlsV1.Docking
{
    public class DockRegionState
    {
        public VitNX_DockArea Area { get; set; }
        public Size Size { get; set; }
        public List<DockGroupState> Groups { get; set; }

        public DockRegionState()
        { Groups = new List<DockGroupState>(); }

        public DockRegionState(VitNX_DockArea area) : this()
        {
            Area = area;
        }

        public DockRegionState(VitNX_DockArea area,
            Size size) : this(area)
        {
            Size = size;
        }
    }
}