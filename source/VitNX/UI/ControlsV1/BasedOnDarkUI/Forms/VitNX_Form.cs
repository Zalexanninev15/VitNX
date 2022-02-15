using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Config;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Forms
{
    public class VitNX_Form : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            Functions.Windows.WindowSAndControls.WindowS.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        private bool _flatBorder;

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

        public VitNX_Form()
        {
            BackColor = Colors.GreyBackground;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (!_flatBorder)
                return;
            var g = e.Graphics;
            using (var p = new Pen(Colors.VitNXBorder))
            {
                var modRect = new Rectangle(ClientRectangle.Location,
                    new Size(ClientRectangle.Width - 1, 
                    ClientRectangle.Height - 1));
                g.DrawRectangle(p, modRect);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 265);
            this.Name = "VitNX_Form";
            this.TopMost = true;
            this.ResumeLayout(false);
        }
    }
}