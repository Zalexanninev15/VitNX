using VitNX.Controls;
using VitNX.Forms;

namespace Example
{
    public partial class DialogControls : VitNXDialog
    {
        public DialogControls()
        {
            InitializeComponent();

            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNXListItem($"List item #{i}");
                lstTest.Items.Add(item);
            }

            // Build dummy nodes
            var childCount = 0;
            for (var i = 0; i < 20; i++)
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

                treeTest.Nodes.Add(node);
            }

            // Hook dialog button events
            btnDialog.Click += delegate
            {
                VitNXMessageBox.ShowError("This is an error", "VitNX UI - Example");
            };

            btnMessageBox.Click += delegate
            {
                VitNXMessageBox.ShowInformation("This is some information, except it is much bigger, so there we go. I wonder how this is going to go. I hope it resizes properly. It probably will.", "VitNX UI - Example");
            };
        }
    }
}