using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Controls;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    internal class VitNX_DockTabArea
    {
        #region Field Region

        private Dictionary<VitNX_DockContent, VitNX_DockTab> _tabs = new Dictionary<VitNX_DockContent, VitNX_DockTab>();

        private List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();
        private VitNX_ContextMenu _tabMenu = new VitNX_ContextMenu();

        #endregion Field Region

        #region Property Region

        public VitNX_DockArea DockArea { get; private set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle DropdownRectangle { get; set; }

        public bool DropdownHot { get; set; }

        public int Offset { get; set; }

        public int TotalTabSize { get; set; }

        public bool Visible { get; set; }

        public VitNX_DockTab ClickedCloseButton { get; set; }

        #endregion Property Region

        #region Constructor Region

        public VitNX_DockTabArea(VitNX_DockArea dockArea)
        {
            DockArea = dockArea;
        }

        #endregion Constructor Region

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

        public ToolStripMenuItem GetMenuItem(VitNX_DockContent content)
        {
            ToolStripMenuItem menuItem = null;
            foreach (ToolStripMenuItem item in _menuItems)
            {
                var menuContent = item.Tag as VitNX_DockContent;
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
                    var content = (VitNX_DockContent)item.Tag;
                    if (content.Order == index)
                        orderedItems.Add(item);
                }
                index++;
            }

            foreach (var item in orderedItems)
                _tabMenu.Items.Add(item);
        }

        #endregion Method Region
    }
}