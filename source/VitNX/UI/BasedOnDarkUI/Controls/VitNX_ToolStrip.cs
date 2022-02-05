using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.BasedOnDarkUI.Renderers;

namespace VitNX.UI.BasedOnDarkUI.Controls
{
    public class VitNX_ToolStrip : ToolStrip
    {
        #region Constructor Region

        public VitNX_ToolStrip()
        {
            Renderer = new VitNX_ToolStripRenderer();
            Padding = new Padding(5, 0, 1, 0);
            AutoSize = false;
            Size = new Size(1, 28);
        }

        #endregion Constructor Region
    }
}