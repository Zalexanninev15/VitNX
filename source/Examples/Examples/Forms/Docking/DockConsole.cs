﻿using VitNX.UI.ControlsV1.BasedOnDarkUI.Controls;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Docking;

namespace Example
{
    public partial class DockConsole : VitNX_ToolWindow
    {
        public DockConsole()
        {
            InitializeComponent();
            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new VitNX_ListItem($"List item #{i}");
                lstConsole.Items.Add(item);
            }
        }
    }
}