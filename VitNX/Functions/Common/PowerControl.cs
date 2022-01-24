using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace VitNX.Functions.Common
{
    public class Power
    {
        public static void ComputerPowerControl(Windows.Win32.Enums.SYSTEM_POWER_CONTROL method)
        {
            switch (method)
            {
                case Windows.Win32.Enums.SYSTEM_POWER_CONTROL.SYSTEM_LOGOFF:
                    {
                        Windows.Win32.Import.ExitWindowsEx(0, 0);
                        break;
                    }
                case Windows.Win32.Enums.SYSTEM_POWER_CONTROL.SYSTEM_SHUTDOWN:
                    {
                        Process.Start("shutdown", "/s /t 0");
                        break;
                    }
                case Windows.Win32.Enums.SYSTEM_POWER_CONTROL.SYSTEM_REBOOT:
                    {
                        Process.Start("shutdown", "/r /t 0");
                        break;
                    }
                case Windows.Win32.Enums.SYSTEM_POWER_CONTROL.SYSTEM_LOCK:
                    {
                        Windows.Win32.Import.LockWorkStation();
                        break;
                    }
            }
        }

        public static void MonitorPowerControl(bool worked)
        {
            Form frm = new Form();
            if (worked)
            {
                Windows.Win32.Import.mouse_event(Windows.Win32.Constants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                frm.Close();
            }
            else Windows.Win32.Import.SendMessage(frm.Handle,
                Windows.Win32.Constants.WM_SYSCOMMAND,
                (IntPtr)Windows.Win32.Constants.SC_MONITORPOWER,
                (IntPtr)2);
        }
    }
}