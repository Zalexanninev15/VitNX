using System.Windows.Forms;

using VitNX.UI.BasedOnDarkUI.Renderers;

namespace VitNX.UI.BasedOnDarkUI.Controls
{
    public class VitNX_ContextMenu : ContextMenuStrip
    {
        public VitNX_ContextMenu()
        { Renderer = new VitNX_MenuRenderer(); }
    }
}