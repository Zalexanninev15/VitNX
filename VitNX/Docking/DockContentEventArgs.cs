using System;

namespace VitNX.Docking
{
    public class DockContentEventArgs : EventArgs
    {
        public VNXDockContent Content { get; private set; }

        public DockContentEventArgs(VNXDockContent content)
        {
            Content = content;
        }
    }
}
