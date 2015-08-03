using Abp.AutoMapper;
using StarBlogs.Blogs.Dtos;
using StarBlogs.Stars;
using StarBlogs.Stars.Dtos;
using System.Collections.Generic;

namespace StarBlogs.Stars.Dtos
{
     [AutoMapFrom(typeof(Star))]
    public class StarWithBlogsDto : StarDto
    {
         public ICollection<BlogDto> Blogs { get; set; }
    }
}
