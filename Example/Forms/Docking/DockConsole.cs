using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockConsole : VitNXToolWindow
    {
        public DockConsole()
        {
            InitializeComponent();
            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNXListItem($"List item #{i}");
                lstConsole.Items.Add(item);
            }
        }
    }
}