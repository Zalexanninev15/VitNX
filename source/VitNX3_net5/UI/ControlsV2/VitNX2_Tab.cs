﻿using System;
using System.Windows.Forms;

namespace VitNX2.UI.ControlsV2
{
    public class VitNX2_Tab : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }
}