using VitNX.Config;
using VitNX.Controls;
using VitNX.Docking;
using VitNX.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Example
{
    public partial class DockDocument : VitNXDocument
    {
        #region Constructor Region

        public DockDocument()
        {
            InitializeComponent();

            // Workaround to stop the textbox from highlight all text.
            txtDocument.SelectionStart = txtDocument.Text.Length;

            // Build dummy dropdown data
            cmbOptions.Items.Add(new VitNXDropdownItem("25%"));
            cmbOptions.Items.Add(new VitNXDropdownItem("50%"));
            cmbOptions.Items.Add(new VitNXDropdownItem("100%"));
            cmbOptions.Items.Add(new VitNXDropdownItem("200%"));
            cmbOptions.Items.Add(new VitNXDropdownItem("300%"));
            cmbOptions.Items.Add(new VitNXDropdownItem("400%"));
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
            var result = VitNXMessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", @"Close document", VitNXDialogButton.YesNo);
            if (result == DialogResult.No)
                return;

            base.Close();
        }

        #endregion
    }
}
