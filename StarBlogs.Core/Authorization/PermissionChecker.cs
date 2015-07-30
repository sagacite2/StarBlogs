using Abp.Authorization;
using StarBlogs.MultiTenancy;
using StarBlogs.Users;

namespace StarBlogs.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}