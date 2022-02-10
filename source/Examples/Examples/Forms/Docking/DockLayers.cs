using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockLayers : VitNX_ToolWindow
    {
        public DockLayers()
        {
            InitializeComponent();
            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                item.Icon = Icons.application_16x;
                lstLayers.Items.Add(item);
            }
            // Build dropdown list data
            for (var i = 0; i < 5; i++) { cmbList.Items.Add(new VitNX_DropdownItem($"Dropdown item #{i}")); }
        }
    }
}