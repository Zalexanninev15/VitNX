using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.BasedOnDarkUI.Config;

namespace VitNX.UI.BasedOnDarkUI.Forms
{
    public class VitNX_Form : Form
    {
        #region Field Region

        protected override void OnHandleCreated(EventArgs e)
        {
            Functions.Windows.WindowS.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        private bool _flatBorder;

        #endregion Field Region

        #region Property Region

        [Category("Appearance")]
        [Description("Determines whether a single pixel border should be rendered around the form.")]
        [DefaultValue(false)]
        public bool FlatBorder
        {
            get { return _flatBorder; }
            set
            {
                _flatBorder = value;
                Invalidate();
            }
        }

        #endregion Property Region

        #region Constructor Region

        public VitNX_Form()
        {
            BackColor = Colors.GreyBackground;
        }

        #endregion Constructor Region

        #region Paint Region

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (!_flatBorder)
                return;

            var g = e.Graphics;

            using (var p = new Pen(Colors.VitNXBorder))
            {
                var modRect = new Rectangle(ClientRectangle.Location, new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1));
                g.DrawRectangle(p, modRect);
            }
        }

        #endregion Paint Region

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // VitNX_Form
            //
            this.ClientSize = new Size(284, 265);
            this.Name = "VitNX_Form";
            this.TopMost = true;
            this.ResumeLayout(false);
        }
    }
}