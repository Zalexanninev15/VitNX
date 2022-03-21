using System;
using System.Windows.Forms;

using VitNX.Functions.WindowAndControls;

namespace VitNX.UI.ControlsV2
{
    public partial class VitNX2_MessageBoxType2 : Form
    {
        public static string MessageText { get; set; }

        public VitNX2_MessageBoxType2()
        {
            InitializeComponent();
            Window.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
            Size = new System.Drawing.Size(309, 247);
            messageRichTextBox.Text = MessageText;
        }

        private void Button1Click(object sender, EventArgs e)
        { Close(); }
    }
}