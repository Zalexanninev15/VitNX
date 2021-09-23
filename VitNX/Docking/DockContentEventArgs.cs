using System;

namespace VitNX.Docking
{
    public class DockContentEventArgs : EventArgs
    {
        public VitNXDockContent Content { get; private set; }

        public DockContentEventArgs(VitNXDockContent content)
        {
            Content = content;
        }
    }
}