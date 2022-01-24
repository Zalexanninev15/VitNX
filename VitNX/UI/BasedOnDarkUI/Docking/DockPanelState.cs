using System.Collections.Generic;

namespace VitNX.Docking
{
    public class DockPanelState
    {
        #region Property Region

        public List<DockRegionState> Regions { get; set; }

        #endregion Property Region

        #region Constructor Region

        public DockPanelState()
        {
            Regions = new List<DockRegionState>();
        }

        #endregion Constructor Region
    }
}