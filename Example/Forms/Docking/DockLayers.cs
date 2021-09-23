using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockLayers : VitNXToolWindow
    {
        public DockLayers()
        {
            InitializeComponent();
            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNXListItem($"List item #{i}");
                item.Icon = Icons.application_16x;
                lstLayers.Items.Add(item);
            }
            // Build dropdown list data
            for (var i = 0; i < 5; i++) { cmbList.Items.Add(new VitNXDropdownItem($"Dropdown item #{i}")); }
        }
    }
}