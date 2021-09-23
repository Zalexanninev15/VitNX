using VitNX.Renderers;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VNXToolStrip : ToolStrip
    {
        #region Constructor Region

        public VNXToolStrip()
        {
            Renderer = new VNXToolStripRenderer();
            Padding = new Padding(5, 0, 1, 0);
            AutoSize = false;
            Size = new Size(1, 28);
        }

        #endregion
    }
}
