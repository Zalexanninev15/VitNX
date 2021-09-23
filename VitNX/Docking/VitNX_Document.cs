using VitNX.Config;
using System.ComponentModel;

namespace VitNX.Docking
{
    [ToolboxItem(false)]
    public class VitNXDocument : VitNXDockContent
    {
        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new VitNXDockArea DefaultDockArea
        {
            get { return base.DefaultDockArea; }
        }

        #endregion

        #region Constructor Region

        public VitNXDocument()
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = VitNXDockArea.Document;
        }

        #endregion
    }
}