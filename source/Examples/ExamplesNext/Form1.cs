using System;
using System.Windows.Forms;

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
    }
}