using System.Drawing;
using System.Windows.Forms;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Win32
{
    public class ControlScrollFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.MOUSEWHEEL:
                case (int)Functions.Windows.Win32.Enums.WINDOW_MESSAGE.MOUSEHWHEEL:
                    var hControlUnderMouse = Functions.Windows.Win32.Import.WindowFromPoint(new Point((int)m.LParam));
                    if (hControlUnderMouse == m.HWnd) { return false; }
                    Functions.Windows.Win32.Import.SendMessage(hControlUnderMouse,
                        (uint)m.Msg,
                        m.WParam,
                        m.LParam);
                    return true;
            }
            return false;
        }
    }
}