using VitNX.UI.ControlsV1.BasedOnDarkUI.Controls;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Docking;

namespace Example
{
    public partial class DockHistory : VitNX_ToolWindow
    {
        public DockHistory()
        {
            InitializeComponent();
            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                lstHistory.Items.Add(item);
            }
        }
    }
}