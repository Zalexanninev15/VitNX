using VitNX.Controls;
using VitNX.Forms;

namespace Example
{
    public partial class DialogControls : VNXDialog
    {
        public DialogControls()
        {
            InitializeComponent();

            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VNXListItem($"List item #{i}");
                lstTest.Items.Add(item);
            }

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

                treeTest.Nodes.Add(node);
            }

            // Hook dialog button events
            btnDialog.Click += delegate
            {
                VNXMessageBox.ShowError("This is an error", "VNX UI - Example");
            };

            btnMessageBox.Click += delegate
            {
                VNXMessageBox.ShowInformation("This is some information, except it is much bigger, so there we go. I wonder how this is going to go. I hope it resizes properly. It probably will.", "VNX UI - Example");
            };
        }
    }
}