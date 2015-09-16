using Abp.Application.Services;
using Abp.Application.Services.Dto;
using StarBlogs.Blogs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
    public interface IPostAppService : IApplicationService
    {
        void DeletePost(DeletePostInput input);
        void BlockPost(BlockPostInput input);
        void DeletePicture(DeletePictureInput input);
        void CreateUpdatePost(CreateUpdatePostInput input);
        PagedResultOutput<PostWithBlogOfStarDto> GetPosts(GetAllPostInput input);
        PagedResultOutput<PostDto> GetPostsForAll(GetAllPostInput input);
        PagedResultDto<PostDto> GetPostsByStar(GetPostByStarInput input);
        PagedResultDto<PostDto> GetPostsByStarForAll(GetPostByStarInput input);
    }
}
