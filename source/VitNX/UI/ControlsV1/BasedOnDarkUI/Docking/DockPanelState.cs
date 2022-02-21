using System.Collections.Generic;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    public class DockPanelState
    {
        public List<DockRegionState> Regions { get; set; }

        public DockPanelState()
        {
            Regions = new List<DockRegionState>();
        }
    }
}