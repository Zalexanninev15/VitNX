﻿using System;

namespace VitNX.UI.ControlsV1.Controls
{
    public class ScrollValueEventArgs : EventArgs
    {
        public int Value { get; private set; }

        public ScrollValueEventArgs(int value)
        {
            Value = value;
        }
    }
}