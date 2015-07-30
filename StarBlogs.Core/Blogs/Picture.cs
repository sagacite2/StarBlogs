using Abp.Domain.Entities;
using Newtonsoft.Json;
using StarBlogs.Stars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
     [Table("Picture")]
    public class Picture:Entity
    {
         [ForeignKey("PostId")]
         [JsonIgnore]
         public virtual OriginalPost Post { get; set; }
         public virtual int PostId { get; set; }
         public virtual int StarId { get; set; }
         public virtual int BlogId { get; set; }
         public virtual string Url { get; set; }
    }
}
