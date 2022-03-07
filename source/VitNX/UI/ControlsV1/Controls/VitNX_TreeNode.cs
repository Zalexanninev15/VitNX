using System;
using System.Drawing;

namespace VitNX.UI.ControlsV1.Controls
{
    public class VitNX_TreeNode
    {
        #region Event Region

        public event EventHandler<ObservableListModified<VitNX_TreeNode>> ItemsAdded;

        public event EventHandler<ObservableListModified<VitNX_TreeNode>> ItemsRemoved;

        public event EventHandler TextChanged;

        public event EventHandler NodeExpanded;

        public event EventHandler NodeCollapsed;

        #endregion Event Region

        #region Field Region

        private string _text;
        private bool _isRoot;
        private VitNX_TreeView _parentTree;
        private VitNX_TreeNode _parentNode;

        private ObservableList<VitNX_TreeNode> _nodes;

        private bool _expanded;

        #endregion Field Region

        #region Property Region

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;

                _text = value;

                OnTextChanged();
            }
        }

        public Rectangle ExpandArea { get; set; }

        public Rectangle IconArea { get; set; }

        public Rectangle TextArea { get; set; }

        public Rectangle FullArea { get; set; }

        public bool ExpandAreaHot { get; set; }

        public Bitmap Icon { get; set; }

        public Bitmap ExpandedIcon { get; set; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded == value)
                    return;

                if (value == true && Nodes.Count == 0)
                    return;

                _expanded = value;

                if (_expanded)
                {
                    if (NodeExpanded != null)
                        NodeExpanded(this, null);
                }
                else
                {
                    if (NodeCollapsed != null)
                        NodeCollapsed(this, null);
                }
            }
        }

        public ObservableList<VitNX_TreeNode> Nodes
        {
            get { return _nodes; }
            set
            {
                if (_nodes != null)
                {
                    _nodes.ItemsAdded -= Nodes_ItemsAdded;
                    _nodes.ItemsRemoved -= Nodes_ItemsRemoved;
                }

                _nodes = value;

                _nodes.ItemsAdded += Nodes_ItemsAdded;
                _nodes.ItemsRemoved += Nodes_ItemsRemoved;
            }
        }

        public bool IsRoot
        {
            get { return _isRoot; }
            set { _isRoot = value; }
        }

        public VitNX_TreeView ParentTree
        {
            get { return _parentTree; }
            set
            {
                if (_parentTree == value)
                    return;

                _parentTree = value;

                foreach (var node in Nodes)
                    node.ParentTree = _parentTree;
            }
        }

        public VitNX_TreeNode ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        public bool Odd { get; set; }

        public object NodeType { get; set; }

        public object Tag { get; set; }

        public string FullPath
        {
            get
            {
                var parent = ParentNode;
                var path = Text;

                while (parent != null)
                {
                    path = string.Format("{0}{1}{2}", parent.Text, "\\", path);
                    parent = parent.ParentNode;
                }

                return path;
            }
        }

        public VitNX_TreeNode PrevVisibleNode { get; set; }

        public VitNX_TreeNode NextVisibleNode { get; set; }

        public int VisibleIndex { get; set; }

        public bool IsNodeAncestor(VitNX_TreeNode node)
        {
            var parent = ParentNode;
            while (parent != null)
            {
                if (parent == node)
                    return true;

                parent = parent.ParentNode;
            }

            return false;
        }

        #endregion Property Region

        #region Constructor Region

        public VitNX_TreeNode()
        {
            Nodes = new ObservableList<VitNX_TreeNode>();
        }

        public VitNX_TreeNode(string text)
            : this()
        {
            Text = text;
        }

        #endregion Constructor Region

        #region Method Region

        public void Remove()
        {
            if (ParentNode != null)
                ParentNode.Nodes.Remove(this);
            else
                ParentTree.Nodes.Remove(this);
        }

        public void EnsureVisible()
        {
            var parent = ParentNode;

            while (parent != null)
            {
                parent.Expanded = true;
                parent = parent.ParentNode;
            }
        }

        #endregion Method Region

        #region Event Handler Region

        private void OnTextChanged()
        {
            if (ParentTree != null && ParentTree.TreeViewNodeSorter != null)
            {
                if (ParentNode != null)
                    ParentNode.Nodes.Sort(ParentTree.TreeViewNodeSorter);
                else
                    ParentTree.Nodes.Sort(ParentTree.TreeViewNodeSorter);
            }

            if (TextChanged != null)
                TextChanged(this, null);
        }

        private void Nodes_ItemsAdded(object sender, ObservableListModified<VitNX_TreeNode> e)
        {
            foreach (var node in e.Items)
            {
                node.ParentNode = this;
                node.ParentTree = ParentTree;
            }

            if (ParentTree != null && ParentTree.TreeViewNodeSorter != null)
                Nodes.Sort(ParentTree.TreeViewNodeSorter);

            if (ItemsAdded != null)
                ItemsAdded(this, e);
        }

        private void Nodes_ItemsRemoved(object sender, ObservableListModified<VitNX_TreeNode> e)
        {
            if (Nodes.Count == 0)
                Expanded = false;

            if (ItemsRemoved != null)
                ItemsRemoved(this, e);
        }

        #endregion Event Handler Region
    }
}