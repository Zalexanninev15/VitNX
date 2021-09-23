using VitNX.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Docking
{
    internal class VitNXDockTabArea
    {
        #region Field Region

        private Dictionary<VitNXDockContent, VitNXDockTab> _tabs = new Dictionary<VitNXDockContent, VitNXDockTab>();

        private List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();
        private VitNXContextMenu _tabMenu = new VitNXContextMenu();

        #endregion

        #region Property Region

        public VitNXDockArea DockArea { get; private set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle DropdownRectangle { get; set; }

        public bool DropdownHot { get; set; }

        public int Offset { get; set; }

        public int TotalTabSize { get; set; }

        public bool Visible { get; set; }

        public VitNXDockTab ClickedCloseButton { get; set; }

        #endregion
        
        #region Constructor Region

        public VitNXDockTabArea(VitNXDockArea dockArea)
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

        public ToolStripMenuItem GetMenuItem(VitNXDockContent content)
        {
            ToolStripMenuItem menuItem = null;
            foreach (ToolStripMenuItem item in _menuItems)
            {
                var menuContent = item.Tag as VitNXDockContent;
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
                    var content = (VitNXDockContent)item.Tag;
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
