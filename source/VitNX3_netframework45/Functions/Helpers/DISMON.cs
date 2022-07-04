using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace VitNX3.Functions.Helpers
{
    public static class DISMON
    {
        public static string UnknownMonitor => "Unknown Monitor";

        public static string[] GetMergedFriendlyNames()
        {
            var namesByMonitorIds = GetNamesByMonitorIds();
            var captions = GetCaptions();
            if (namesByMonitorIds.Length == 0)
                return captions;
            if (captions.Length == 0 || namesByMonitorIds.Length != captions.Length)
                return namesByMonitorIds;
            return namesByMonitorIds.Select((nameByMonitorId, i) => captions[i] == UnknownMonitor ? nameByMonitorId : captions[i]).ToArray();
        }

        public static string[] GetNamesByMonitorIds()
        {
            var result = new List<string>();
            ManagementObjectSearcher searcher = null;
            ManagementObjectCollection monitors = null;
            try
            {
                var scope = new ManagementScope("\\\\.\\ROOT\\WMI");
                var query = new ObjectQuery("SELECT * FROM WmiMonitorID");
                searcher = new ManagementObjectSearcher(scope, query);
                monitors = searcher.Get(); if (monitors.Count > 0)
                {
                    foreach (var monitor in monitors)
                    {
                        string userFriendlyName = monitor["UserFriendlyName"].AsString();
                        result.Add(!string.IsNullOrEmpty(userFriendlyName) && !userFriendlyName.Contains("PnP") ? userFriendlyName : UnknownMonitor);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                monitors?.Dispose();
                searcher?.Dispose();
            }
            return result.ToArray();
        }

        private static string[] GetCaptions()
        {
            var result = new List<string>();
            ManagementObjectSearcher searcher = null;
            ManagementObjectCollection monitors = null;
            try
            {
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor");
                monitors = searcher.Get();
                if (monitors.Count > 0)
                {
                    foreach (var monitor in monitors)
                    {
                        string caption = monitor["Caption"]?.ToString().Trim();
                        result.Add(!string.IsNullOrEmpty(caption) && !caption.Contains("PnP") ? caption : UnknownMonitor);
                    }
                }
            }
            catch (Exception)
            { }
            finally
            {
                monitors?.Dispose();
                searcher?.Dispose();
            }
            return result.ToArray();
        }

        private static string AsString(this object obj)
        {
            switch (obj)
            {
                case null:
                case IReadOnlyList<ushort> decArray when decArray.Count == 0 || decArray[0] == 0:
                    return string.Empty;

                case IReadOnlyList<ushort> decArray:
                    {
                        var sb = new StringBuilder();
                        foreach (ushort val in decArray)
                        {
                            if (val == 0)
                                break;
                            if (val >= 32 && val <= 127)
                                sb.Append((char)val);
                        }
                        return sb.ToString().Trim();
                    }
                default: return string.Empty;
            }
        }
    }
}