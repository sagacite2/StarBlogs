using Abp.Application.Services;
using StarBlogs.Blogs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
    public interface IBlogAppService : IApplicationService
    {
        void CreateUpdateBlog(CreateUpdateBlogInput input);
    }
}
