using System;
using System.Windows.Forms;

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

        private void vitNX2_Button2_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllLines(VitNX3.Functions.FileSystem.File.NameGenerator("CPU", "txt"),
                VitNX3.Functions.Information.Cpu.Characteristics());
        }

        private void vitNX2_Button3_Click(object sender, EventArgs e)
        {
            Processes.RunAW("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart");
            Processes.RunAW("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart /all");
        }
    }
}