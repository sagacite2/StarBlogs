using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
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
using System.Threading.Tasks;

namespace StarBlogs.Stars
{
    [AbpAuthorize]
    public class StarAppService : ApplicationService, IStarAppService
    {
        private readonly IStarRepository _starRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<StarTagSetting> _starTagSettingRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public StarAppService(IStarRepository starRepository, IRepository<Blog> blogRepository, IRepository<User, long> userRepository, IRepository<StarTagSetting> starTagSettingRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _starRepository = starRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _starTagSettingRepository = starTagSettingRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// 根据ID获取单个明星
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public StarDto GetStar(GetDeleteBlockStarInput input)
        {
            return (_starRepository.Get(input.Id)).MapTo<StarDto>();
        }
        
        /// <summary>
        /// 删除明星及其微博
        /// </summary>
        /// <param name="input"></param>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void DeleteStar(GetDeleteBlockStarInput input)
        {
            var blogs = _starRepository.Get(input.Id).Blogs;
            //这里不能使用foreach，否则出现“集合已修改；可能无法执行枚举操作”错误
            for (int i = 0; i > blogs.Count() - 1; i--)
            {
                _blogRepository.Delete(blogs.Last());
            }
            _starRepository.Delete(input.Id);

        }
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void BlockStar(GetDeleteBlockStarInput input)
        {
            var star = _starRepository.Get(input.Id);
            star.IsBlocked = input.IsBlocked;
        }
        /// <summary>
        /// 获取明星列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public PagedResultOutput<StarDto> GetStars(GetStarsInput input)
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

            return new PagedResultOutput<StarDto>
            {
                TotalCount = starCount,
                Items = stars.MapTo<List<StarDto>>()
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
        public List<StarTagSettingDto> GetStarTagSettings()
        {
            var settings = _starTagSettingRepository.GetAll().ToList();
           // return Mapper.DynamicMap<List<StarTagSetting>, List<StarTagSettingDto>>(settings);
            return settings.MapTo<List<StarTagSettingDto>>();
        }
        public void UpdateStarTag(UpdateStarTagInput input)
        {
            var star = _starRepository.Get(input.StarId);
            if (star == null)
                throw new UserFriendlyException("错误的明星ID！");
            foreach(var tag in star.Tags)
            {
                if (input.StarTags.Find(r => r.TagId == tag.TagId) == null)
                {
                    tag.IsDeleted = true;
                }
            }
            foreach (var tag in input.StarTags)
            {
                star.Tags.Add(new StarTag
                {
                    StarId = tag.StarId,
                    TagId = tag.TagId
                });
            }
            
            _unitOfWorkManager.Current.SaveChanges();
        }
    }
}
