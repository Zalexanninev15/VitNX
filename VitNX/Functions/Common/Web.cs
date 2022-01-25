using System.Net;

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
            catch { return "⚠️ Сервис не отвечает!\nПовторите попытку позже"; }
        }

        public static SecurityProtocolType UseProtocols = SecurityProtocolType.Tls13 |
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls;
    }
}