using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
    [AutoMapFrom(typeof(OriginalPost))]
    public class PostWithBlogDto : PostDto
    {
        public BlogDto Blog { get; set; }
    }
}
