using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using StarBlogs.Authorization;
using StarBlogs.Blogs.Dtos;
using StarBlogs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Blogs
{
    [AbpAuthorize]
    public class PostTranslateService : ApplicationService, IPostTranslateService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OriginalPost> _postRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public PostTranslateService(IRepository<User, long> userRepository, IRepository<OriginalPost> postRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        [AbpAuthorize(PermissionNames.CanManageStars)]
        public void DeletePost(DeletePostInput input)
        {
            _postRepository.Delete(input.Id);
        }
    }
}
