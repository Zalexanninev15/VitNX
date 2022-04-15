using System.ComponentModel;

using VitNX.UI.ControlsV1.Config;

namespace VitNX.UI.ControlsV1.Docking
{
    [ToolboxItem(false)]
    public class VitNX_Document : VitNX_DockContent
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new VitNX_DockArea DefaultDockArea
        {
            get { return base.DefaultDockArea; }
        }

        public VitNX_Document()
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = VitNX_DockArea.Document;
        }
    }
}