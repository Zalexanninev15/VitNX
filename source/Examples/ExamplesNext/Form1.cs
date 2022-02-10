using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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