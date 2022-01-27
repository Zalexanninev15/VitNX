using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace VitNX.Functions.Common.Web
{
    public class DataFromSites
    {
        public static string GetGeo(string ip)
        {
            try
            {
                using (WebClient client = new WebClient())
                    return client.DownloadString($"https://ipinfo.io/{ip}/json");
            }
            catch { return "404"; }
        }

        public static bool IsValidTelegramBotToken(string botToken)
        {
            try
            {
                using (WebClient client = new WebClient())
                    return !client.DownloadString(new Uri($"https://api.telegram.org/bot{botToken}/getMe")).Contains("error_code");
            }
            catch { return false; }
        }

        public static int IHaveInternet()
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("google.com");
                if (pingReply.Status == IPStatus.Success)
                    return 1;
                else
                    return -1;
            }
            catch { return 0; }
        }
    }

    public class Config
    {
        public static string Host = Dns.GetHostName();
        public static string LocalIPv6 = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        public static string LocalIPv4 = Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString();
        public static string _PublicIP = "0.0.0.0";

        public static void Set()
        {
            using (WebClient client = new WebClient())
            {
                try { _PublicIP = client.DownloadString("https://icanhazip.com"); } 
                catch { _PublicIP = client.DownloadString("https://checkip.amazonaws.com"); }
                if (_PublicIP == "" || _PublicIP == "0.0.0.0")
                {
                    try { _PublicIP = client.DownloadString("https://checkip.amazonaws.com"); } catch { }
                }
            }
            _PublicIP = _PublicIP.Trim();
        }

        public static IPAddress DefaultGateway = NetworkInterface
        .GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus == OperationalStatus.Up)
        .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
        .Select(g => g?.Address)
        .Where(a => a != null)
        .FirstOrDefault();

        public static SecurityProtocolType UseProtocols = SecurityProtocolType.Tls13 |
        SecurityProtocolType.Tls12 |
        SecurityProtocolType.Tls11 |
        SecurityProtocolType.Tls;
    }
}