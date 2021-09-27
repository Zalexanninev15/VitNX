using System;
using System.Drawing;
using System.Windows.Forms;
using VitNX.Win32;

namespace VitNX.Forms
{
    internal class VitNX_TranslucentForm : Form
    {
        protected override void OnHandleCreated(EventArgs e) { if (NativeFunctions.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { NativeFunctions.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }

        #region Property Region

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        #endregion

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

        #endregion
    }
}
