using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockProperties : VNXToolWindow
    {
        #region Constructor Region

        public DockProperties()
        {
            InitializeComponent();

            // Build dummy dropdown data
            cmbList.Items.Add(new VNXDropdownItem("Item1"));
            cmbList.Items.Add(new VNXDropdownItem("Item2"));
            cmbList.Items.Add(new VNXDropdownItem("Item3"));
            cmbList.Items.Add(new VNXDropdownItem("Item4"));
            cmbList.Items.Add(new VNXDropdownItem("Item5"));
            cmbList.Items.Add(new VNXDropdownItem("Item6"));

            cmbList.SelectedItemChanged += delegate { System.Console.WriteLine($"Item changed to {cmbList.SelectedItem.Text}"); };
        }

        #endregion
    }
}
