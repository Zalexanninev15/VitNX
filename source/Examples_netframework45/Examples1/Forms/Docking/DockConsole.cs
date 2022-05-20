using VitNX.UI.ControlsV1.Controls;
using VitNX.UI.ControlsV1.Docking;

namespace Examples1
{
    public partial class DockConsole : VitNX_ToolWindow
    {
        public DockConsole()
        {
            InitializeComponent();
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                lstConsole.Items.Add(item);
            }
        }
    }
}