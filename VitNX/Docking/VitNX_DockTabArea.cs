using VitNX.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Docking
{
    internal class VNXDockTabArea
    {
        #region Field Region

        private Dictionary<VNXDockContent, VNXDockTab> _tabs = new Dictionary<VNXDockContent, VNXDockTab>();

        private List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();
        private VNXContextMenu _tabMenu = new VNXContextMenu();

        #endregion

        #region Property Region

        public VNXDockArea DockArea { get; private set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle DropdownRectangle { get; set; }

        public bool DropdownHot { get; set; }

        public int Offset { get; set; }

        public int TotalTabSize { get; set; }

        public bool Visible { get; set; }

        public VNXDockTab ClickedCloseButton { get; set; }

        #endregion
        
        #region Constructor Region

        public VNXDockTabArea(VNXDockArea dockArea)
        {
            DockArea = dockArea;
        }

        #endregion

        #region Method Region

        public void ShowMenu(Control control, Point location)
        {
            _tabMenu.Show(control, location);
        }

        public void AddMenuItem(ToolStripMenuItem menuItem)
        {
            _menuItems.Add(menuItem);
            RebuildMenu();
        }

        public void RemoveMenuItem(ToolStripMenuItem menuItem)
        {
            _menuItems.Remove(menuItem);
            RebuildMenu();
        }

        public ToolStripMenuItem GetMenuItem(VNXDockContent content)
        {
            ToolStripMenuItem menuItem = null;
            foreach (ToolStripMenuItem item in _menuItems)
            {
                var menuContent = item.Tag as VNXDockContent;
                if (menuContent == null)
                    continue;

                if (menuContent == content)
                    menuItem = item;
            }

            return menuItem;
        }

        public void RebuildMenu()
        {
            _tabMenu.Items.Clear();

            var orderedItems = new List<ToolStripMenuItem>();

            var index = 0;
            for (var i = 0; i < _menuItems.Count; i++)
            {
                foreach (var item in _menuItems)
                {
                    var content = (VNXDockContent)item.Tag;
                    if (content.Order == index)
                        orderedItems.Add(item);
                }
                index++;
            }

            foreach (var item in orderedItems)
                _tabMenu.Items.Add(item);
        }

        #endregion
    }
}
