using StarBlogs.Stars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivony.Html.Parser;
using Ivony.Html;
using System.Net;
using System.IO;
using StarBlogs.Blogs;

namespace StarBlogs.Scrawler
{
    public class TwitterScawler
    {
        public string UserName { get; set; }
        public TwitterScawler(string userName)
        {
            UserName = userName;
        }
        public Blog LoadStar(string blogUrl,string imgPath)
        {
            Blog blog = new Blog();
            GC.Collect();
            ServicePointManager.DefaultConnectionLimit = 200;
            HttpWebRequest request = HttpWebRequestFactory.CreateSimpleRequest(blogUrl);
            WebProxy proxy = new WebProxy("127.0.0.1", 1080);
            request.Proxy = proxy;
            try
            {
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                string result = "";
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                {
                    result = reader.ReadToEnd();
                }
                var document = new JumonyParser().Parse(result);
                blog.Name = document.FindFirst(".ProfileHeaderCard-nameLink").InnerHtml();
                blog.Description = document.FindFirst(".ProfileHeaderCard-bio").InnerHtml();
                string imgUrl = document.FindFirst(".ProfileAvatar-image").Attribute("src").Value();

                request = HttpWebRequestFactory.CreateSimpleRequest(imgUrl);
                HttpWebResponse imageResponse = (HttpWebResponse)request.GetResponse(); //反馈请求
                Stream srr = imageResponse.GetResponseStream();
                string path = imgPath + blog.Name.ToString() + ".jpg";
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                //造一个字节类型的数组来存放图片
                byte[] buff = new byte[512];
                int c = 0;
                while ((c = srr.Read(buff, 0, buff.Length)) > 0)
                {
                    fs.Write(buff, 0, c);
                }
                srr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return blog;
        }
    }
}
