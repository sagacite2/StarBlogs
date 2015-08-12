using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.UI;
using StarBlogs.Authorization;
using StarBlogs.Blogs;
using StarBlogs.Configuration;
using StarBlogs.Stars.Dtos;
using StarBlogs.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace StarBlogs.Stars
{
    [AbpAuthorize]
    public class StarAppService : ApplicationService, IStarAppService
    {
        private readonly IStarRepository _starRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<StarTagSetting> _starTagSettingRepository;
        private readonly IRepository<StarTag> _starTagRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public StarAppService(
            IStarRepository starRepository,
            IRepository<Blog> blogRepository,
            IRepository<User, long> userRepository,
            IRepository<StarTagSetting> starTagSettingRepository,
            IRepository<StarTag> starTagRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _starRepository = starRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _starTagSettingRepository = starTagSettingRepository;
            _starTagRepository = starTagRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// 根据ID获取单个明星
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public StarWithBlogsDto GetStar(GetDeleteBlockStarInput input)
        {
            return (_starRepository.Get(input.Id)).MapTo<StarWithBlogsDto>();
        }

        /// <summary>
        /// 删除明星及其微博
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void DeleteStar(GetDeleteBlockStarInput input)
        {
            var star = _starRepository.Get(input.Id);
            if (star == null)
            {
                throw new UserFriendlyException("错误的明星ID！");
            }
            var blogs = star.Blogs;
            //这里不能使用foreach，否则出现“集合已修改；可能无法执行枚举操作”错误
            for (int i = 0; i > blogs.Count() - 1; i--)
            {
                _blogRepository.Delete(blogs.Last());
            }
            _starRepository.Delete(input.Id);

        }
        /// <summary>
        /// 屏蔽或解屏蔽明星，被屏蔽的明星及其微博都不应展示到前台页面
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void BlockStar(GetDeleteBlockStarInput input)
        {
            var star = _starRepository.Get(input.Id);
            star.IsBlocked = input.IsBlocked;
        }
        /// <summary>
        /// 获取明星列表并排序、分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public PagedResultOutput<StarWithBlogsDto> GetStars(GetStarsInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = SettingManager.GetSettingValue<int>(MySettingProvider.StarsListDefaultPageSize);
            }
            var starCount = _starRepository.Count();
            var stars =
                _starRepository
                    .GetAll()
                    .Include(q => q.CreatorUser)
                    .Include(q => q.Blogs)
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToList();

            return new PagedResultOutput<StarWithBlogsDto>
            {
                TotalCount = starCount,
                Items = stars.MapTo<List<StarWithBlogsDto>>()
            };
        }

        /// <summary>
        /// 编辑一个明星，不包括编辑其微博信息
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void UpdateStar(CreateUpdateStarInput input)
        {
            //Logger.Info("Updating a star for input: " + input);

            var star = _starRepository.Get(input.Id);
            star.Gender = input.Gender;
            star.ChineseName = input.ChineseName;
            star.Nickname = input.Nickname;
            star.Name = input.Name;
            star.Description = input.Description;
            star.LastUpdateTime = DateTime.Now;
            //star.IsBlocked = input.IsBlocked;//编辑明星时也可以进行屏蔽操作,暂不搞

        }
        /// <summary>
        /// 添加一个明星，包括属于明星的微博等信息
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void CreateStar(CreateUpdateStarInput input)
        {
            Logger.Info("添加明星的输入信息（CreateUpdateStarInput对象）: " + input);

            var star = new Star
            {
                Name = input.Name,
                Nickname = input.Nickname,
                ChineseName = input.ChineseName,
                Gender = input.Gender,
                Description = input.Description,
                IsBlocked = false,//添加明星默认不屏蔽
                CreationTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,

            };
            //添加该明星对应的首个博客（一个明星对应多个博客，在创建明星时只能添加一个博客，创建完之后才可以增加更多）
            _starRepository.Insert(star);
            _unitOfWorkManager.Current.SaveChanges();
            var blog = new Blog
            {
                Url = input.BlogUrl,
                CreationTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StarId = star.Id,
            };
            blog.ResolveUrl(input.BlogUrl);
            var sameBlog = _blogRepository.GetAll().Where(b => b.Name == blog.Name).Where(b => b.Provider == blog.Provider);
            if (sameBlog.Count() > 0)
            {
                //保证微博地址在数据库中的唯一性，爬虫就不会重复下载博文
                //抛出异常后上面_starRepository.Insert(star)的操作会被回滚
                throw new UserFriendlyException("你添加的微博" + input.BlogUrl + "已经存在，不能重复添加");

            }
            else
            {
                _blogRepository.Insert(blog);
            }
        }
        /// <summary>
        /// 获得标签配置数据
        /// </summary>
        /// <returns></returns>
        public List<StarTagSettingDto> GetStarTagSettings()
        {
            var settings = _starTagSettingRepository.GetAll().ToList();
            // return Mapper.DynamicMap<List<StarTagSetting>, List<StarTagSettingDto>>(settings);
            return settings.MapTo<List<StarTagSettingDto>>();
        }
        /// <summary>
        /// 为明星附加一个标签列表，替代原列表
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void UpdateStarTag(UpdateStarTagInput input)
        {
            var star = _starRepository.Get(input.StarId);
            if (star == null)
                throw new UserFriendlyException("错误的明星ID！");
            //对明星标签的修改操作实际上是不会非常频繁出现的，为求简便，索性把之前的数据全清除
            _starTagRepository.Delete(r => r.StarId == star.Id);
            _unitOfWorkManager.Current.SaveChanges();
            input.StarTags.ForEach(delegate(StarTagDto tag)
            {
                //在Application层使用Entity实体是没问题的，而且应该只让Entity Mapto EntityDto，不要反过来。
                //所以这里不使用tag.Mapto<StarTag>()，因为StarTag类并没有[AutoMapFrom(typeof(StarTagDto))]特性，会报错
                //也不建议为StarTag加入这个特性。
                _starTagRepository.Insert(new StarTag
                {
                    StarId = tag.StarId,
                    TagId = tag.TagId
                });
            });

        }
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void CreateTagSetting(CreateTagSettingInput input)
        {
            _starTagSettingRepository.Insert(new StarTagSetting
            {
                ParentTagId = 0,
                TagName = input.Name
            });
        }
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void UpdateTagSetting(UpdateTagSettingInput input)
        {
            var tag = _starTagSettingRepository.Get(input.Id);
            if (tag!=null)
            {
                tag.TagName = input.Name;
            }
        }
    }
}
