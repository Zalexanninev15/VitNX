﻿using System.Windows.Forms;

using VitNX.UI.ControlsV1.Forms;

namespace Examples1
{
    public partial class DialogAbout : VitNX_Dialog
    {
        public DialogAbout()
        {
            InitializeComponent();
            lblVersion.Text = $"Version: {Application.ProductVersion}";
            btnOk.Text = "Close";
        }
    }
}