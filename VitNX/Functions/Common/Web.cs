using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace VitNX.Functions.Common
{
    public class Web
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
    }

    public class Ip
    {
        public static string host = "localhost";
        public static string localIp = "0.0.0.0";
        public static string publicIp = "0.0.0.0";

        public static void Set()
        {
            using (WebClient client = new WebClient())
            {
                try { publicIp = client.DownloadString("https://icanhazip.com"); } catch { publicIp = client.DownloadString("https://checkip.amazonaws.com"); }
                if (publicIp == "" || localIp == "0.0.0.0")
                {
                    try { publicIp = client.DownloadString("https://checkip.amazonaws.com"); } catch { }
                }
            }
            host = Dns.GetHostName();
            publicIp = localIp.ToString().Replace("\n", "").Replace("\r", "");
            localIp = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        }

        public static IPAddress _defaultGateway = NetworkInterface
        .GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus == OperationalStatus.Up)
        .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
        .Select(g => g?.Address)
        .Where(a => a != null)
        .FirstOrDefault();
    }

    public class Config
    {
        public static SecurityProtocolType _useProtocols = SecurityProtocolType.Tls13 |
        SecurityProtocolType.Tls12 |
        SecurityProtocolType.Tls11 |
        SecurityProtocolType.Tls;

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
}