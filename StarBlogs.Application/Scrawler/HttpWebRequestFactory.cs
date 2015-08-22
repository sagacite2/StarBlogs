using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Scrawler
{
    public class HttpWebRequestFactory
    {
        public static HttpWebRequest CreateSimpleRequest(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //  request.AllowAutoRedirect = true;
            // request.MediaType = "text/html";
            // request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
            request.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 42.0.2311.135 Safari / 537.36 Edge / 12.10240";
            //    request.Method = "GET";
            //   request.Accept = "*/*";
            //   request.ContentType = "application/x-www-form-urlencoded";
            return request;
        }
    }
}
