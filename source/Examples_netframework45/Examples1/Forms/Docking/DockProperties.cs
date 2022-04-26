using VitNX.UI.ControlsV1.Controls;
using VitNX.UI.ControlsV1.Docking;

namespace Examples1
{
    public partial class DockProperties : VitNX_ToolWindow
    {
        public DockProperties()
        {
            InitializeComponent();
            cmbList.Items.Add(new VitNX_DropdownItem("Item1"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item2"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item3"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item4"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item5"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item6"));
            cmbList.SelectedItemChanged += delegate
            {
                System.Console.WriteLine($"Item changed to {cmbList.SelectedItem.Text}");
            };
        }
    }
}