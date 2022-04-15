using System;
using System.Drawing;
using System.Windows.Forms;

using VitNX3.Functions.WindowAndControls;

namespace VitNX.UI.ControlsV1.Forms
{
    internal class VitNX_TranslucentForm : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            Window.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public VitNX_TranslucentForm(Color backColor,
            double opacity = 0.6)
        {
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Size = new Size(1, 1);
            ShowInTaskbar = false;
            AllowTransparency = true;
            Opacity = opacity;
            BackColor = backColor;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 265);
            this.Name = "VitNX_TranslucentForm";
            this.TopMost = true;
            this.ResumeLayout(false);
        }
    }
}