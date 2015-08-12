using Abp.Domain.Entities.Auditing;
using Newtonsoft.Json;
using StarBlogs.Stars;
using StarBlogs.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
    [Table("Translation")]
    public class Translation : CreationAuditedEntity<int, User>
    {
        public virtual OriginalPost OriginalPost { get; set; }
        public virtual int OriginalPostId { get; set; }
        public virtual int StarId { get; set; }
        public virtual int BlogId { get; set; }
        public virtual string Content { get; set; }
        public virtual int VoteUp { get; set; }
        public virtual int VoteDown { get; set; }
        public virtual int InheritedEditionId { get; set; }
        public virtual int CommentCount { get; set; }
       
    }
}
