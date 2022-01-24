using System.Windows.Forms;
using VitNX.Renderers;

namespace VitNX.Controls
{
    public class VitNX_ContextMenu : ContextMenuStrip
    {
        public VitNX_ContextMenu()
        { Renderer = new VitNX_MenuRenderer(); }
    }
}