using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Win32
{
    public class ControlScrollFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WM.MOUSEWHEEL:
                case (int)WM.MOUSEHWHEEL:
                    var hControlUnderMouse = NativeFunctions.WindowFromPoint(new Point((int)m.LParam));
                    if (hControlUnderMouse == m.HWnd) { return false; }
                    NativeFunctions.SendMessage(hControlUnderMouse, (uint)m.Msg, m.WParam, m.LParam);
                    return true;
            }
            return false;
        }
    }
}