using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockProject : VNXToolWindow
    {
        #region Constructor Region

        public DockProject()
        {
            InitializeComponent();

            // Build dummy nodes
            var childCount = 0;
            for (var i = 0; i < 20; i++)
            {
                var node = new VNXTreeNode($"Root node #{i}");
                node.ExpandedIcon = Icons.folder_open;
                node.Icon = Icons.folder_closed;

                for (var x = 0; x < 10; x++)
                {
                    var childNode = new VNXTreeNode($"Child node #{childCount}");
                    childNode.Icon = Icons.files;
                    childCount++;
                    node.Nodes.Add(childNode);
                }

                treeProject.Nodes.Add(node);
            }
        }

        #endregion
    }
}
