using Abp.Authorization.Roles;
using StarBlogs.MultiTenancy;
using StarBlogs.Users;

namespace StarBlogs.Authorization
{
    public class Role : AbpRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}