using System.Collections.Generic;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    public class DockGroupState
    {
        public List<string> Contents { get; set; }
        public string VisibleContent { get; set; }

        public DockGroupState()
        {
            Contents = new List<string>();
        }
    }
}