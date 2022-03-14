using VitNX.UI.ControlsV1.Controls;
using VitNX.UI.ControlsV1.Docking;

namespace Example
{
    public partial class DockLayers : VitNX_ToolWindow
    {
        public DockLayers()
        {
            InitializeComponent();
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                item.Icon = Icons.application_16x;
                lstLayers.Items.Add(item);
            }
            for (var i = 0; i < 5; i++)
                cmbList.Items.Add(new VitNX_DropdownItem($"Dropdown item #{i}"));
        }
    }
}