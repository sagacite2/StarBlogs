using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using StarBlogs.Users.Dto;
using StarBlogs.Users.Dtos;
using System.Threading.Tasks;
using Abp.UI;

namespace StarBlogs.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly UserManager _userManager;

        public UserAppService(UserManager userManager)
        {
            _userManager = userManager;
        }

        public ListResultOutput<UserDto> GetUsers()
        {
            return new ListResultOutput<UserDto>
            {
                Items = _userManager.Users.ToList().MapTo<List<UserDto>>()
            };
        }
        public async void Register(RegisterInput input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Name = input.UserName,
                    Password = input.Password,
                    Surname = "",
                    UserName = input.UserName,
                    EmailAddress = "default@undefined.com"

                });
                if (result.Succeeded)
                {
                    await _userManager.LoginAsync(input.UserName, input.Password);
                }
                else
                {
                    throw new UserFriendlyException("创建用户时出现错误!");
                }
            }
            else
            {
                throw new UserFriendlyException("该用户名已经被注册!");
            }
            
        }
        public bool CheckUserName()
        {
            return false;
        }
    }
}