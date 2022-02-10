using System;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.UI.BasedOnDarkUI.Forms
{
    internal class VitNX_TranslucentForm : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            Functions.Windows.WindowS.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        #region Property Region

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        #endregion Property Region

        #region Constructor Region

        public VitNX_TranslucentForm(Color backColor, double opacity = 0.6)
        {
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Size = new Size(1, 1);
            ShowInTaskbar = false;
            AllowTransparency = true;
            Opacity = opacity;
            BackColor = backColor;
        }

        #endregion Constructor Region

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // VitNX_TranslucentForm
            //
            this.ClientSize = new System.Drawing.Size(284, 265);
            this.Name = "VitNX_TranslucentForm";
            this.TopMost = true;
            this.ResumeLayout(false);
        }
    }
}