﻿using System.Windows.Forms;

using VitNX.UI.ControlsV1.Renderers;

namespace VitNX.UI.ControlsV1.Controls
{
    public class VitNX_MenuStrip : MenuStrip
    {
        public VitNX_MenuStrip()
        {
            Renderer = new VitNX_MenuRenderer();
            Padding = new Padding(3, 2, 0, 2);
        }
    }
}