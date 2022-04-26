using VitNX.UI.ControlsV1.Controls;
using VitNX.UI.ControlsV1.Docking;

namespace Examples1
{
    public partial class DockHistory : VitNX_ToolWindow
    {
        public DockHistory()
        {
            InitializeComponent();
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                lstHistory.Items.Add(item);
            }
        }
    }
}