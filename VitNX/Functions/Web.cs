using System.Net;

namespace VitNX.Functions
{
    public class Web
    {
        public static string GetGeoInString(string ip)
        {
            try
            {
                using (WebClient client = new WebClient())
                    return client.DownloadString($"https://ipinfo.io/{ip}/json");
            }
            catch { return "⚠️ Сервис не отвечает!\nПовторите попытку позже"; }
        }
    }
}