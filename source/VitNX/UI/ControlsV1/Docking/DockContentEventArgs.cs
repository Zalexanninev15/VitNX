using System;

namespace VitNX.UI.ControlsV1.Docking
{
    public class DockContentEventArgs : EventArgs
    {
        public VitNX_DockContent Content { get; private set; }

        public DockContentEventArgs(VitNX_DockContent content)
        {
            Content = content;
        }
    }
}