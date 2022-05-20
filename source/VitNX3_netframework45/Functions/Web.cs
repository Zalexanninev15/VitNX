using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace VitNX3.Functions.Web
{
    /// <summary>
    /// Works with data from sites.
    /// </summary>
    public class DataFromSites
    {
        /// <summary>
        /// Downloads the string from site/server.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="ifError">Text is returned if no string can be retrieved</param>
        /// <returns>A string.</returns>
        public static string DownloadString(string url, string ifError = "404")
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Proxy = null;
                    return Data.Text.FixDeEncoding(client.DownloadString(url));
                }
            }
            catch { return ifError; }
        }

        /// <summary>
        /// Downloads the file with support of download resume.
        /// </summary>
        /// <param name="sourceFileUrl">The source file url.</param>
        /// <param name="targetFile">The target file.</param>
        public static void DownloadFileWithSupportOfResume(string sourceFileUrl,
            string targetFile)
        {
            long iFileSize = 0;
            int iBufferSize = 1024;
            iBufferSize *= 1000;
            long iExistLen = 0;
            FileStream saveFileStream;
            if (File.Exists(targetFile))
            {
                FileInfo fINfo = new FileInfo(targetFile);
                iExistLen = fINfo.Length;
            }
            if (iExistLen > 0)
                saveFileStream = new FileStream(targetFile,
                  FileMode.Append,
                  FileAccess.Write,
                  FileShare.ReadWrite);
            else
                saveFileStream = new FileStream(targetFile,
                  FileMode.Create,
                  FileAccess.Write,
                  FileShare.ReadWrite);
            HttpWebRequest hwRq;
            HttpWebResponse hwRes;
            hwRq = (HttpWebRequest)WebRequest.Create(sourceFileUrl);
            hwRq.AddRange((int)iExistLen);
            Stream smRespStream;
            hwRes = (HttpWebResponse)hwRq.GetResponse();
            smRespStream = hwRes.GetResponseStream();
            iFileSize = hwRes.ContentLength;
            int iByteSize;
            byte[] downBuffer = new byte[iBufferSize];
            while ((iByteSize = smRespStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                saveFileStream.Write(downBuffer, 0, iByteSize);
            saveFileStream.Close();
        }

        /// <summary>
        /// Gets the status header and content of site.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="userAgent">The UserAgent.</param>
        /// <returns>An array of string (Header, Content).</returns>
        public static string[] GetHeaderAndContent(string url, string userAgent)
        {
            string[] data = { "Header", "Content" };
            HttpWebRequest wReq;
            HttpWebResponse wResp;
            Stream rStream;
            wReq = (HttpWebRequest)WebRequest.Create(url);
            wReq.KeepAlive = false;
            wReq.Referer = url;
            wReq.UserAgent = userAgent;
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
        public static string Post(string url,
            string options)
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

        /// <summary>
        /// Starts the uploading.
        /// Example:https://gist.github.com/Zalexanninev15/13df3d870ee9951b65081212cde9863a
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="values">The values.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="completed">The completed.</param>
        public static void FileUploader(string url,
            NameValueCollection values,
            string filePath,
            Action<string, int> progress,
            Action<string> completed)
        {
            var fileStream = File.OpenRead(filePath);
            var fileName = Path.GetFileName(filePath);
            var ms = new MemoryStream();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            try
            {
                const string contentType = "application/octet-stream";
                var request = WebRequest.Create(url);
                request.Method = "POST";
                var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                boundary = "--" + boundary;
                var dataStream = new MemoryStream();
                byte[] buffer;
                foreach (string name in values.Keys)
                {
                    buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    dataStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}",
                        name, Environment.NewLine));
                    dataStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    dataStream.Write(buffer, 0, buffer.Length);
                }
                buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                dataStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes($"Content-Disposition: form-data; name=\"file\"; filename=\"{fileName}\"{Environment.NewLine}");
                dataStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}",
                    contentType,
                    Environment.NewLine));
                dataStream.Write(buffer, 0, buffer.Length);
                ms.CopyTo(dataStream);
                buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                dataStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(boundary + "--");
                dataStream.Write(buffer, 0, buffer.Length);
                dataStream.Position = 0;
                request.ContentLength = dataStream.Length;
                var requestStream = request.GetRequestStream();
                var size = dataStream.Length;
                const int chunkSize = 64 * 1024;
                buffer = new byte[chunkSize];
                long bytesSent = 0;
                int readBytes;
                while ((readBytes = dataStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    requestStream.Write(buffer, 0, readBytes);
                    bytesSent += readBytes;
                    var status = "Uploading... " + bytesSent / 1024 + "KB of " + size / 1024 + "KB";
                    var percentage = Convert.ToInt32(100 * bytesSent / size);
                    progress(status, percentage);
                }
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var stream = new MemoryStream())
                {
                    responseStream.CopyTo(stream);
                    var result = Encoding.Default.GetString(stream.ToArray());
                    completed(result == string.Empty
                        ? "failed:" + result
                        : "ok:" + result);
                }
            }
            catch (Exception e) { completed(e.ToString()); }
        }
    }

    /// <summary>
    /// The configs for normal work with sites and Internet.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Activate all security protocols for all network functions to work (HTTPS).
        /// Example: ServicePointManager.SecurityProtocol = VitNX3.Functions.Web.Config.UseProtocols();
        /// </summary>
        ///
        public static SecurityProtocolType UseProtocols() => SecurityProtocolType.Tls12 |
        SecurityProtocolType.Tls11 |
        SecurityProtocolType.Tls;

        /// <summary>
        /// Activate DefaultGateway of NetworkInterface in IPAddress.
        /// </summary>
        public static IPAddress DefaultGateway() => NetworkInterface
        .GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus == OperationalStatus.Up)
        .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
        .Select(g => g?.Address).FirstOrDefault(a => a != null);
    }
}