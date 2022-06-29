using System;
using System.Windows.Forms;

using VitNX3.Functions.AppsAndProcesses;

namespace Examples2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void vitNX2_ToogleButton1_CheckedChanged(object sender, EventArgs e)
        {
            vitNX2_Tab1.SelectedIndex = 1;
        }

        private void vitNX2_Button1_Click(object sender, EventArgs e)
        {
            vitNX2_Tab1.SelectedIndex = 0;
        }

        private void vitNX2_Button3_Click(object sender, EventArgs e)
        {
            Processes.RunAW("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart");
            Processes.RunAW("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart /all");
        }

        private void vitNX2_Button4_Click(object sender, EventArgs e)
        {
            VitNX2.UI.ControlsV2.VitNX2_MessageBox.Show("World is very Big!", "Hello", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}