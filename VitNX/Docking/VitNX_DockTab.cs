using System.Drawing;

namespace VitNX.Docking
{
    internal class VNXDockTab
    {
        #region Property Region

        public VNXDockContent DockContent { get; set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle CloseButtonRectangle { get; set; }

        public bool Hot { get; set; }

        public bool CloseButtonHot { get; set; }

        public bool ShowSeparator { get; set; }

        #endregion

        #region Constructor Region

        public VNXDockTab(VNXDockContent content)
        {
            DockContent = content;
        }

        #endregion

        #region Method Region

        public int CalculateWidth(Graphics g, Font font)
        {
            var width = (int)g.MeasureString(DockContent.DockText, font).Width;
            width += 10;

            return width;
        }

        #endregion
    }
}
