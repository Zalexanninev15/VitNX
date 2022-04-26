using System.Windows.Forms;

using VitNX.UI.ControlsV1.Renderers;

namespace VitNX.UI.ControlsV1.Controls
{
    public class VitNX_ContextMenu : ContextMenuStrip
    {
        public VitNX_ContextMenu()
        { Renderer = new VitNX_MenuRenderer(); }
    }
}