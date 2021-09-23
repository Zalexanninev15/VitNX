using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockProperties : VitNXToolWindow
    {
        #region Constructor Region

        public DockProperties()
        {
            InitializeComponent();

            // Build dummy dropdown data
            cmbList.Items.Add(new VitNXDropdownItem("Item1"));
            cmbList.Items.Add(new VitNXDropdownItem("Item2"));
            cmbList.Items.Add(new VitNXDropdownItem("Item3"));
            cmbList.Items.Add(new VitNXDropdownItem("Item4"));
            cmbList.Items.Add(new VitNXDropdownItem("Item5"));
            cmbList.Items.Add(new VitNXDropdownItem("Item6"));

            cmbList.SelectedItemChanged += delegate { System.Console.WriteLine($"Item changed to {cmbList.SelectedItem.Text}"); };
        }

        #endregion
    }
}
