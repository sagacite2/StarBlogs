using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs.Dtos
{
    public class CreateUpdateBlogInput : IInputDto
    {
        public int starId;
        public ICollection<string> BlogUrls;
        public ICollection<int> BlogIds;
    }
}
