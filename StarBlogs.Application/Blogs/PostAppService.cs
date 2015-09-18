using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Linq.Extensions;
using StarBlogs.Authorization;
using StarBlogs.Blogs.Dtos;
using StarBlogs.Configuration;
using StarBlogs.Users;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System;

namespace StarBlogs.Blogs
{
    [AbpAuthorize]
    public class PostAppService : ApplicationService, IPostAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OriginalPost> _postRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public PostAppService(
            IRepository<User, long> userRepository,
            IRepository<OriginalPost> postRepository,
            IRepository<Picture> pictureRepository, 
             IRepository<Blog> blogRepository,

            IUnitOfWorkManager unitOfWorkManager)
        {

            _userRepository = userRepository;
            _postRepository = postRepository;
            _pictureRepository = pictureRepository;
            _blogRepository = blogRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// 删除一篇博文
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void DeletePost(DeletePostInput input)
        {

            var post = _postRepository.Get(input.Id);
            if (post == null)
                throw new UserFriendlyException("错误的博文ID");
            var pics = post.Pictures;
            for (int i = pics.Count(); i > 0; i--)
            {
                _pictureRepository.Delete(pics.Last());
            }
            _postRepository.Delete(input.Id);
        }
        /// <summary>
        /// 屏蔽或解屏蔽一篇博文
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void BlockPost(BlockPostInput input)
        {
            var post = _postRepository.Get(input.Id);
            if (post == null)
                throw new UserFriendlyException("错误的博文ID");
            post.IsBlocked = input.IsBlocked;
            //_unitOfWorkManager.Current.SaveChanges();
        }
        /// <summary>
        /// 根据ID删除一张博文图片
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void DeletePicture(DeletePictureInput input)
        {
            _pictureRepository.Delete(input.Id);
        }
        /// <summary>
        /// 获得所有博文并排序、分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public PagedResultOutput<PostWithBlogOfStarDto> GetPosts(GetAllPostInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.PostsListDefaultPageSize);
            }
            var postCount = _postRepository.Count();
            var posts =
                _postRepository
                    .GetAll()
                    .Include(q => q.Pictures)
                    .Include(q => q.Blog)
                    .Include("Blog.Star")
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToList();

            return new PagedResultOutput<PostWithBlogOfStarDto>
            {
                TotalCount = postCount,
                Items = posts.MapTo<List<PostWithBlogOfStarDto>>()
            };
        }
        /// <summary>
        /// 获得所有未屏蔽的博文，用于前台展示
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultOutput<PostWithBlogOfStarDto> GetPostsForAll(GetAllPostInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.PostsListDefaultPageSize);
            }
            
            var posts =
                _postRepository
                    .GetAll()
                    .Where(r => !r.IsBlocked)
                     .Include(q => q.Pictures)
                    .Include(q => q.Blog)
                    .Include("Blog.Star")
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToList();
            var postCount = _postRepository
                    .GetAll()
                    .Where(r => !r.IsBlocked).Count();
            return new PagedResultOutput<PostWithBlogOfStarDto>
            {
                TotalCount = postCount,
                Items = posts.MapTo<List<PostWithBlogOfStarDto>>()
            };
        }
        /// <summary>
        /// 获得某明星的所有博文并分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public PagedResultDto<PostWithBlogOfStarDto> GetPostsByStar(GetPostByStarInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.PostsListDefaultPageSize);
            }
            
            var posts =
                _postRepository
                    .GetAll()
                    .Where(r => r.StarId == input.StarId)
                     .Include(q => q.Pictures)
                    .Include(q => q.Blog)
                    .Include("Blog.Star")
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToList();
            var postCount = _postRepository
                    .GetAll()
                    .Where(r => r.StarId == input.StarId).Count();
            return new PagedResultDto<PostWithBlogOfStarDto>
            {
                TotalCount = postCount,
                Items = posts.MapTo<List<PostWithBlogOfStarDto>>()
            };
        }
        /// <summary>
        /// 获得某个明星的未屏蔽博文并分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<PostWithBlogOfStarDto> GetPostsByStarForAll(GetPostByStarInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.PostsListDefaultPageSize);
            }
           
            var posts =
                _postRepository
                    .GetAll()
                    .Where(r => r.StarId == input.StarId && !r.IsBlocked)
                    .Include(q => q.Pictures)
                    .Include(q => q.Blog)
                    .Include("Blog.Star")
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToList();
            var postCount = _postRepository
                    .GetAll()
                    .Where(r => r.StarId == input.StarId && !r.IsBlocked).Count();
            return new PagedResultOutput<PostWithBlogOfStarDto>
            {
                TotalCount = postCount,
                Items = posts.MapTo<List<PostWithBlogOfStarDto>>()
            };
        }
        /// <summary>
        /// 添加一条博文
        /// </summary>
        /// <param name="input"></param>
        public void CreateUpdatePost(CreateUpdatePostInput input)
        {
            var blog = _blogRepository.Get(input.BlogId);
            if (blog != null)
            {
                var post = new OriginalPost();
                post.Content = input.Content;
                post.DefaultTranslate = input.DefaultTranslate;
                post.IsBlocked = false;
                post.PostTime = DateTime.Now;
                post.StarId = blog.StarId;
                post.BlogId = blog.Id;
                _postRepository.Insert(post);
            }
        }
    }
}
