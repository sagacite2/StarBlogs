using Abp.Authorization;
using Abp.Localization;

namespace StarBlogs.Authorization
{
    public class StarAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.CanManageStars, new FixedLocalizableString("可以管理明星"));
            context.CreatePermission(PermissionNames.CanManageTranslatings, new FixedLocalizableString("可以管理译文"));

        }
    }
}
