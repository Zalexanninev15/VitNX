using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Renderers;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Controls
{
    public class VitNX_ContextMenu : ContextMenuStrip
    {
        public VitNX_ContextMenu()
        { Renderer = new VitNX_MenuRenderer(); }
    }
}