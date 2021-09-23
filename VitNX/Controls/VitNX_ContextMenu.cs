using VitNX.Renderers;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VNXContextMenu : ContextMenuStrip
    {
        #region Constructor Region

        public VNXContextMenu()
        {
            Renderer = new VNXMenuRenderer();
        }

        #endregion
    }
}
