using VitNX.UI.ControlsV1.Controls;
using VitNX.UI.ControlsV1.Forms;

namespace Example
{
    public partial class DialogControls : VitNX_Dialog
    {
        public DialogControls()
        {
            InitializeComponent();
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                lstTest.Items.Add(item);
            }
            var childCount = 0;
            for (var i = 0; i < 20; i++)
            {
                var node = new VitNX_TreeNode($"Root node #{i}");
                node.ExpandedIcon = Icons.folder_open;
                node.Icon = Icons.folder_closed;
                for (var x = 0; x < 10; x++)
                {
                    var childNode = new VitNX_TreeNode($"Child node #{childCount}");
                    childNode.Icon = Icons.files;
                    childCount++;
                    node.Nodes.Add(childNode);
                }
                treeTest.Nodes.Add(node);
            }
            btnDialog.Click += delegate { VitNX_MessageBox.ShowError("This is a Error", "VitNX UI - Example"); };
            btnMessageBox.Click += delegate
            {
                VitNX_MessageBox.ShowInfo("This is some information, except it is much bigger, so there we go. I wonder how this is going to go. I hope it resizes properly. It probably will.",
                    "VitNX UI - Example");
            };
        }
    }
}