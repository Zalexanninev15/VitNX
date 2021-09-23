﻿using VitNX.Controls;
using VitNX.Docking;

namespace Example
{
    public partial class DockHistory : DarkToolWindow
    {
        #region Constructor Region

        public DockHistory()
        {
            InitializeComponent();

            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new DarkListItem($"List item #{i}");
                lstHistory.Items.Add(item);
            }
        }

        #endregion
    }
}
