using VitNX.Renderers;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNXMenuStrip : MenuStrip
    {
        public VitNXMenuStrip() { Renderer = new VitNXMenuRenderer(); Padding = new Padding(3, 2, 0, 2); }
    }
}