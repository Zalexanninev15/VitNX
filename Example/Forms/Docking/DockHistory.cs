using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockHistory : VitNXToolWindow
    {
        public DockHistory()
        {
            InitializeComponent();
            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNXListItem($"List item #{i}");
                lstHistory.Items.Add(item);
            }
        }
    }
}