using VitNX.Renderers;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNXContextMenu : ContextMenuStrip
    {
        #region Constructor Region

        public VitNXContextMenu()
        {
            Renderer = new VitNXMenuRenderer();
        }

        #endregion
    }
}
