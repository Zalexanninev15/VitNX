using System.Collections.Generic;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    public class DockGroupState
    {
        #region Property Region

        public List<string> Contents { get; set; }

        public string VisibleContent { get; set; }

        #endregion Property Region

        #region Constructor Region

        public DockGroupState()
        {
            Contents = new List<string>();
        }

        #endregion Constructor Region
    }
}