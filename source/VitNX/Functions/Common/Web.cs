using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace VitNX.Functions.Common.Web
{
    /// <summary>
    /// Work with data from sites.
    /// </summary>
    public class DataFromSites
    {
        /// <summary>
        /// Downloads the string from site/server.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>A string.</returns>
        public static string DownloadString(string url)
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

        /// <summary>
        /// Gets the header and content of site.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="useragent">The useragent.</param>
        /// <returns>An array of string.</returns>
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

        /// <summary>
        /// Gets the geolocation of PC.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns>A string.</returns>
        public static string GetGeo(string ip)
        {
            try
            {
                using (WebClient client = new WebClient())
                    return client.DownloadString($"https://ipinfo.io/{ip}/json");
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// Are the valid telegram bot token.
        /// </summary>
        /// <param name="botToken">The bot token.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidTelegramBotToken(string botToken)
        {
            try
            {
                using (WebClient client = new WebClient())
                    return !client.DownloadString(new Uri($"https://api.telegram.org/bot{botToken}/getMe")).Contains("error_code");
            }
            catch { return false; }
        }

        /// <summary>
        /// Are yout have the internet connection on PC.
        /// </summary>
        /// <returns>An int.</returns>
        public static int IsHaveInternet()
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

    /// <summary>
    /// Work with config of PC.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets the host name (name of PC, Windows System).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetHostName() => Dns.GetHostName();

        /// <summary>
        /// Gets the local IPv6 (obsolete, but work).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetLocalIPv6() => Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

        /// <summary>
        /// Gets the local IPv4 (obsolete, but work).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetLocalIPv4() => Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString();

        /// <summary>
        /// Gets the public IP of PC.
        /// </summary>
        public static string GetPublicIP()
        {
            string PublicIP = "localhost";
            using (WebClient client = new WebClient())
            {
                try { PublicIP = client.DownloadString("https://icanhazip.com"); }
                catch { PublicIP = client.DownloadString("https://checkip.amazonaws.com"); }
                if (PublicIP == "" || PublicIP == "0.0.0.0" || PublicIP == "localhost")
                {
                    try { PublicIP = client.DownloadString("https://checkip.amazonaws.com"); } catch { PublicIP = "0.0.0.0"; }
                }
            }
            return PublicIP.Trim();
        }

        /// <summary>
        /// Get DefaultGateway of NetworkInterface in IPAddress.
        /// </summary>
        public static IPAddress DefaultGateway() => NetworkInterface
        .GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus == OperationalStatus.Up)
        .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
        .Select(g => g?.Address).FirstOrDefault(a => a != null);

        /// <summary>
        /// Activate all security protocols for all network functions to work (HTTPS).
        /// </summary>
        ///
        public static SecurityProtocolType UseProtocols() => SecurityProtocolType.Tls12 |
        SecurityProtocolType.Tls11 |
        SecurityProtocolType.Tls;

        // Code for .NET Framework 4.8+
        // public static SecurityProtocolType UseProtocols = SecurityProtocolType.Tls13 |
        // SecurityProtocolType.Tls12 |
        // SecurityProtocolType.Tls11 |
        // SecurityProtocolType.Tls;
    }

    /// <summary>
    /// The send data to sites.
    /// </summary>
    public class SendDataToSites
    {
        /// <summary>
        /// Using POST request to send text data.
        ///  Example: string request = Post("https://site.com/auth", "client_id=43435&key=create");
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="options">The options.</param>
        /// <returns>A string.</returns>
        public static string Post(string url, string options)
        {
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