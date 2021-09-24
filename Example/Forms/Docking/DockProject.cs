using VitNX.Controls;
using VitNX.Docking;
using System.Threading;

namespace Example
{
    public partial class DockProject : VitNXToolWindow
    {
        public DockProject()
        {
            InitializeComponent();
            // Build dummy nodes
            var childCount = 0;
            for (var i = 0; i < 10; i++)
            {
                var node = new VitNXTreeNode($"Root node #{i}");
                node.ExpandedIcon = Icons.folder_open;
                node.Icon = Icons.folder_closed;
                for (var x = 0; x < 10; x++)
                {
                    var childNode = new VitNXTreeNode($"Child node #{childCount}");
                    childNode.Icon = Icons.files;
                    childCount++;
                    node.Nodes.Add(childNode);
                }
                treeProject.Nodes.Add(node);
            }
        }

        private void vitNXButton1_Click(object sender, System.EventArgs e) { vitNXProgressBarStyle21.Value = 0; vitNXProgressBar1.Value = 0; timer1.Start(); }
        private void timer1_Tick(object sender, System.EventArgs e) { vitNXProgressBar1.Increment(1); vitNXProgressBarStyle21.Increment(1); }
    }
}