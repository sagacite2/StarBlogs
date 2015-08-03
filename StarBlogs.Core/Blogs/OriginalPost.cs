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
    [Table("OriginalPost")]
    public class OriginalPost:Entity
    {

        public virtual int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual int StarId { get; set; }
        //public virtual Star Star { get; set; }

        public virtual string Content { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual DateTime PostTime { get; set; }

        public virtual string DefaultTranslate { get; set; }

        public virtual bool IsBlocked { get; set; }
    }
}
