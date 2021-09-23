using VitNX.Config;
using VitNX.Controls;
using VitNX.Docking;
using VitNX.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Example
{
    public partial class DockDocument : VNXDocument
    {
        #region Constructor Region

        public DockDocument()
        {
            InitializeComponent();

            // Workaround to stop the textbox from highlight all text.
            txtDocument.SelectionStart = txtDocument.Text.Length;

            // Build dummy dropdown data
            cmbOptions.Items.Add(new VNXDropdownItem("25%"));
            cmbOptions.Items.Add(new VNXDropdownItem("50%"));
            cmbOptions.Items.Add(new VNXDropdownItem("100%"));
            cmbOptions.Items.Add(new VNXDropdownItem("200%"));
            cmbOptions.Items.Add(new VNXDropdownItem("300%"));
            cmbOptions.Items.Add(new VNXDropdownItem("400%"));
        }

        public DockDocument(string text, Image icon)
            : this()
        {
            DockText = text;
            Icon = icon;
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            var result = VNXMessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", @"Close document", VNXDialogButton.YesNo);
            if (result == DialogResult.No)
                return;

            base.Close();
        }

        #endregion
    }
}
