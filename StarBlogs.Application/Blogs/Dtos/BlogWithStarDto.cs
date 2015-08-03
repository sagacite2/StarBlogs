using Abp.AutoMapper;
using StarBlogs.Stars;
using StarBlogs.Stars.Dtos;

namespace StarBlogs.Blogs.Dtos
{
    [AutoMapFrom(typeof(Blog))]
    public class BlogWithStarDto : BlogDto
    {
        public StarDto Star { get; set; }
    }
}
