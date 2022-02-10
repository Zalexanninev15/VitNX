using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockProperties : VitNX_ToolWindow
    {
        public DockProperties()
        {
            InitializeComponent();
            // Build dummy dropdown data
            cmbList.Items.Add(new VitNX_DropdownItem("Item1"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item2"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item3"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item4"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item5"));
            cmbList.Items.Add(new VitNX_DropdownItem("Item6"));
            cmbList.SelectedItemChanged += delegate { System.Console.WriteLine($"Item changed to {cmbList.SelectedItem.Text}"); };
        }
    }
}