using VitNX.Renderers;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNXToolStrip : ToolStrip
    {
        #region Constructor Region

        public VitNXToolStrip()
        {
            Renderer = new VitNXToolStripRenderer();
            Padding = new Padding(5, 0, 1, 0);
            AutoSize = false;
            Size = new Size(1, 28);
        }

        #endregion
    }
}