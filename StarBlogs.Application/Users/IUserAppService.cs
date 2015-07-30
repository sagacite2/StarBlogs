using Abp.Application.Services;
using Abp.Application.Services.Dto;
using StarBlogs.Users.Dto;

namespace StarBlogs.Users
{
    public interface IUserAppService : IApplicationService
    {
        ListResultOutput<UserDto> GetUsers();
    }
}
