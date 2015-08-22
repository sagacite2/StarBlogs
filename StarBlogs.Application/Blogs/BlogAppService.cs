using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Timing;
using Abp.UI;
using StarBlogs.Authorization;
using StarBlogs.Blogs.Dtos;
using StarBlogs.Stars;
using StarBlogs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
    [AbpAuthorize]
    public class BlogAppService : ApplicationService, IBlogAppService
    {
        private readonly IStarRepository _starRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public BlogAppService(IStarRepository starRepository, IRepository<Blog> blogRepository, IRepository<User, long> userRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _starRepository = starRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void CreateUpdateBlog(CreateUpdateBlogInput input)
        {
            //System.Web.HttpContext.Current.Server.MapPath(
            var star = _starRepository.Get(input.starId);
            if (star == null)
            {
                throw new UserFriendlyException("错误的明星ID！");
            }
            if(input.BlogUrls.Count()<3 || input.BlogIds.Count()<3)
                throw new UserFriendlyException("地址信息至少要有3条！");
            for (int i = 0; i < 3; i++)
            {
                var url = input.BlogUrls.ElementAt(i);
                var id = input.BlogIds.ElementAt(i);
                if (string.IsNullOrEmpty(url))
                {
                    if (id != -1)
                    {
                        //把已有的URL置为空字符串，这意味着管理者想删除该微博信息
                        _blogRepository.Delete(id);
                    }
                }
                else
                {
                    var blog = new Blog { Url = url };
                    blog.ResolveUrl(url);
                    if (!string.IsNullOrEmpty(blog.Name) && blog.Provider != BlogProvider.UNDEFINED)
                    {
                        var sameBlog = _blogRepository.GetAll().Where(b => b.StarId != input.starId && b.Name == blog.Name && b.Provider == blog.Provider);
                        if (sameBlog.Count() > 0)
                        {
                            throw new UserFriendlyException("你添加的微博" + url + "已经存在，不能重复添加");
                        }
                    }
                    if (id != -1)
                    {
                        //编辑微博
                        var existBlog = _blogRepository.Get(id);
                        if (existBlog == null)
                        {
                            throw new UserFriendlyException("错误的微博ID！");
                        }
                        existBlog.LastUpdateTime = DateTime.Now;
                        existBlog.Url = blog.Url;
                        existBlog.Name = blog.Name;
                        existBlog.Provider = blog.Provider;
                        existBlog.ProviderName = blog.ProviderName;
                        _blogRepository.Update(existBlog);
                    }
                    else
                    {
                        //添加微博
                        blog.CreationTime = DateTime.Now;
                        blog.LastUpdateTime = DateTime.Now;
                        blog.StarId = input.starId;
                        blog.Description = "";
                        _blogRepository.Insert(blog);
                    }
                }
            }
        }
    }
}
