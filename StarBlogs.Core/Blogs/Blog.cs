using Abp.Domain.Entities;
using StarBlogs.Stars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace StarBlogs.Blogs
{
    [Table("Blog")]
    public class Blog : Entity
    {
        /// <summary>
        /// 博客地址
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 博客提供商，枚举
        /// </summary>
        public virtual BlogProvider Provider { get; set; }
        /// <summary>
        /// 博客提供商，名称，从URL中提取，如facebook之类
        /// </summary>
        public virtual string ProviderName { get; set; }
        /// <summary>
        /// 博客名称，即明星在该站点的标识ID
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 所属明星
        /// </summary>
        //[JsonIgnore]//加上JsonIgnore特性避免出现Self referencing loop detected问题
        public virtual Star Star { get; set; }
        /// <summary>
        /// 所属明星Id
        /// </summary>
        public virtual int StarId { get; set; }
        /// <summary>
        /// 博客简介
        /// </summary>
        public virtual string Description { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual DateTime LastUpdateTime { get; set; }
      //  [JsonIgnore]
      //  public virtual ICollection<OriginalPost> Posts { get; set; }
        /// <summary>
        /// 从博客地址抽取Provider、Name信息
        /// 如地址：https://twitter.com/XHNews
        /// 则抽取Provider = "TWITTER"
        /// Name = XHNews
        /// </summary>
        /// <param name="url"></param>
        public void ResolveUrl(string url)
        {
            Regex r = new Regex(@"^([a-zA-Z]+:\/\/)?([^\/]+)\.com\/(.+)\/?.*?$");
            Match m = r.Match(url);
            if (m != null && m.Groups.Count > 3)
            {
                this.ProviderName = m.Groups[2].Value.ToLower();
                this.Provider = (BlogProvider)Enum.Parse(typeof(BlogProvider), m.Groups[2].Value.ToUpper());
                this.Name = m.Groups[3].Value;
            }
            else
            {
                this.Provider = BlogProvider.UNDEFINED;
                this.Name = "";
            }
        }
    }
}
