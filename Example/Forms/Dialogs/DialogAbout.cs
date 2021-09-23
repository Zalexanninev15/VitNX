using VitNX.Forms;
using System.Windows.Forms;

namespace Example
{
    public partial class DialogAbout : VitNXDialog
    {
        #region Constructor Region

        public DialogAbout()
        {
            InitializeComponent();

            lblVersion.Text = $"Version: {Application.ProductVersion.ToString()}";
            btnOk.Text = "Close";
        }

        #endregion
    }
}
