using VitNX3.Functions.AppsAndProcesses;
using VitNX3.Functions.Win32;

namespace VitNX3.Functions
{
    /// <summary>
    /// Work with the power control.
    /// </summary>
    public class PowerControl
    {
        /// <summary>
        /// Options for control the power of computer.
        /// </summary>
        public enum SYSTEM_POWER_CONTROL
        {
            SYSTEM_LOGOFF,
            SYSTEM_SHUTDOWN,
            SYSTEM_REBOOT,
            SYSTEM_LOCK
        }

        /// <summary>
        /// The power of computer.
        /// </summary>
        /// <param name="method">The method.</param>
        public static void Computer(SYSTEM_POWER_CONTROL method)
        {
            switch (method)
            {
                case SYSTEM_POWER_CONTROL.SYSTEM_LOGOFF:
                    {
                        Import.ExitWindowsEx(0, 0);
                        break;
                    }
                case SYSTEM_POWER_CONTROL.SYSTEM_SHUTDOWN:
                    {
                        // Old code: Processes.Run("shutdown", "/s /t 0");
                        Processes.Run("powershell", "Stop-Computer");
                        break;
                    }
                case SYSTEM_POWER_CONTROL.SYSTEM_REBOOT:
                    {
                        // Old code: Processes.Run("shutdown", "/r /t 0");
                        Processes.Run("powershell", "Restart-Computer -Force");
                        break;
                    }
                case SYSTEM_POWER_CONTROL.SYSTEM_LOCK:
                    {
                        Import.LockWorkStation();
                        break;
                    }
            }
        }
    }
}