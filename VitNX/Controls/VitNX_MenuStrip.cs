using VitNX.Renderers;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VNXMenuStrip : MenuStrip
    {
        #region Constructor Region

        public VNXMenuStrip()
        {
            Renderer = new VNXMenuRenderer();
            Padding = new Padding(3, 2, 0, 2);
        }

        #endregion
    }
}
