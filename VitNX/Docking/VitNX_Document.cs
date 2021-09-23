using VitNX.Config;
using System.ComponentModel;

namespace VitNX.Docking
{
    [ToolboxItem(false)]
    public class VNXDocument : VNXDockContent
    {
        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new VNXDockArea DefaultDockArea
        {
            get { return base.DefaultDockArea; }
        }

        #endregion

        #region Constructor Region

        public VNXDocument()
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = VNXDockArea.Document;
        }

        #endregion
    }
}
