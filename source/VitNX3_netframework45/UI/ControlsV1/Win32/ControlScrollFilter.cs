using System.Drawing;
using System.Windows.Forms;

using VitNX3.Functions.Win32;

namespace VitNX.UI.ControlsV1.Win32
{
    public class ControlScrollFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)Enums.WINDOW_MESSAGE.MOUSE_WHEEL:
                case (int)Enums.WINDOW_MESSAGE.MOUSE_H_WHEEL:
                    var hControlUnderMouse = Import.WindowFromPoint(new Point((int)m.LParam));
                    if (hControlUnderMouse == m.HWnd) { return false; }
                    Import.SendMessage(hControlUnderMouse,
                        (uint)m.Msg,
                        m.WParam,
                        m.LParam);
                    return true;
            }
            return false;
        }
    }
}