using System.ComponentModel;

using VitNX.UI.BasedOnDarkUI.Config;

namespace VitNX.UI.BasedOnDarkUI.Docking
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

        #endregion Property Region

        #region Constructor Region

        public VitNX_Document()
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = VitNX_DockArea.Document;
        }

        #endregion Constructor Region
    }
}