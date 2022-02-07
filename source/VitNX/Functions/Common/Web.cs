using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace VitNX.Functions.Common.Web
{
    public class DataFromSites
    {
        public static string DownloadString(string url, bool get = false, string options = "")
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Proxy = null;
                    return Text.Work.FixDeEncoding(client.DownloadString(url));
                }
            }
            catch (Exception ex) { return ex.Message; }
        }

        public static string[] GetHeaderAndContent(string url, string useragent)
        {
            string[] data = { "Header", "Content" };
            HttpWebRequest wReq;
            HttpWebResponse wResp;
            Stream rStream;
            wReq = (HttpWebRequest)WebRequest.Create(url);
            wReq.KeepAlive = false;
            wReq.Referer = url;
            wReq.UserAgent = useragent;
            wResp = (HttpWebResponse)wReq.GetResponse();
            data[0] = wResp.Headers.ToString();
            rStream = wResp.GetResponseStream();
            int bufCount = 0;
            byte[] byteBuf = new byte[1024];
            do
            {
                bufCount = rStream.Read(byteBuf, 0, byteBuf.Length);
                if (bufCount != 0)
                    data[1] += Encoding.ASCII.GetString(byteBuf, 0, bufCount);
            }
            while (bufCount > 0);
            return data;
        }

        public static string GetGeo(string ip)
        {
            try
            {
                using (WebClient client = new WebClient())
                    return client.DownloadString($"https://ipinfo.io/{ip}/json");
            }
            catch (Exception ex) { return ex.Message; }
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
        public static string _PublicIP = "localhost";

        public static void Set()
        {
            using (WebClient client = new WebClient())
            {
                try { _PublicIP = client.DownloadString("https://icanhazip.com"); }
                catch { _PublicIP = client.DownloadString("https://checkip.amazonaws.com"); }
                if (_PublicIP == "" || _PublicIP == "0.0.0.0" || _PublicIP == "localhost")
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

        public static bool OpenLink(string link)
        {
            try
            {
                Process.Start(link);
                return true;
            }
            catch { return false; }
        }
    }

    public class SendDataToSites
    {
        public static string Post(string url, string options)
        {
            // Example: Post("https://site.com/auth", "client_id=43435&key=create");
            try
            {
                WebRequest req = WebRequest.Create(url);
                req.Method = "POST";
                req.Timeout = 100000;
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] sentData = Encoding.UTF8.GetBytes(options);
                req.ContentLength = sentData.Length;
                Stream sendStream = req.GetRequestStream();
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
                WebResponse res = req.GetResponse();
                Stream ReceiveStream = res.GetResponseStream();
                StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
                char[] read = new char[256];
                int count = sr.Read(read, 0, 256);
                string Out = string.Empty;
                while (count > 0)
                {
                    string str = new string(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
                return Out;
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}