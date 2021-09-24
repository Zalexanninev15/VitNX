using VitNX.Forms;
using System.Windows.Forms;

namespace Example
{
    public partial class DialogAbout : VitNX_Dialog
    {
        public DialogAbout()
        {
            InitializeComponent();
            lblVersion.Text = $"Version: {Application.ProductVersion}";
            btnOk.Text = "Close";
        }
    }
}