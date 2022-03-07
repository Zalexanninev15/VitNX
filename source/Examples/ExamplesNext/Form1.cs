using System;
using System.Windows.Forms;

using VitNX.Functions.AppsAndProcesses;

namespace ExamplesNext
{
    public partial class Form1 : Form
    {
        public Form1()
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
            System.IO.File.WriteAllLines(VitNX.Functions.FileSystem.FileNameGenerator("CPU", "txt"),
                VitNX.Functions.Information.Cpu.Characteristics());
        }

        private void vitNX2_Button3_Click(object sender, EventArgs e)
        {
            Processes.RunAW("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart");
            Processes.RunAW("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart /all");
        }
    }
}