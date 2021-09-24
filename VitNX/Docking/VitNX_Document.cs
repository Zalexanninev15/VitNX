using VitNX.Config;
using System.ComponentModel;

namespace VitNX.Docking
{
    [ToolboxItem(false)]
    public class VitNX_Document : VitNX_DockContent
    {
        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new VitNX_DockArea DefaultDockArea
        {
            get { return base.DefaultDockArea; }
        }

        #endregion

        #region Constructor Region

        public VitNX_Document()
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = VitNX_DockArea.Document;
        }

        #endregion
    }
}