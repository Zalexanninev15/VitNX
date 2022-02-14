using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Controls;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Docking;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Forms;

namespace Example
{
    public partial class DockDocument : VitNX_Document
    {
        public DockDocument()
        {
            InitializeComponent();
            txtDocument.SelectionStart = txtDocument.Text.Length;
            cmbOptions.Items.Add(new VitNX_DropdownItem("25%"));
            cmbOptions.Items.Add(new VitNX_DropdownItem("50%"));
            cmbOptions.Items.Add(new VitNX_DropdownItem("100%"));
            cmbOptions.Items.Add(new VitNX_DropdownItem("200%"));
            cmbOptions.Items.Add(new VitNX_DropdownItem("300%"));
            cmbOptions.Items.Add(new VitNX_DropdownItem("400%"));
        }

        public DockDocument(string text, Image icon) : this()
        {
            DockText = text; Icon = icon;
        }

        public override void Close()
        {
            var result = VitNX_MessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", 
                @"Close document", 
                VitNX_DialogButton.YesNo);
            if (result == DialogResult.No) 
                return;
            base.Close();
        }
    }
}