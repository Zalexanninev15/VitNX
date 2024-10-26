﻿using System.Drawing;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.Config;

namespace VitNX.UI.ControlsV1.Controls
{
    public class VitNX_Separator : Control
    {
        #region Constructor Region

        public VitNX_Separator()
        {
            SetStyle(ControlStyles.Selectable, false);

            Dock = DockStyle.Top;
            Size = new Size(1, 2);
        }

        #endregion Constructor Region

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            using (var p = new Pen(Colors.VitNXBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
            }

            using (var p = new Pen(Colors.LightBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 1, ClientRectangle.Right, 1);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Absorb event
        }

        #endregion Paint Region
    }
}