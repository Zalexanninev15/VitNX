using VitNX.Renderers;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNX_MenuStrip : MenuStrip
    {
        public VitNX_MenuStrip() { Renderer = new VitNX_MenuRenderer(); Padding = new Padding(3, 2, 0, 2); }
    }
}