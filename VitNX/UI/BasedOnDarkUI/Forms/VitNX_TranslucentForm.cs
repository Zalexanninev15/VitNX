using System;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.UI.BasedOnDarkUI.Forms
{
    internal class VitNX_TranslucentForm : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            if (Functions.Windows.Win32.Import.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                Functions.Windows.Win32.Import.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
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
    }
}