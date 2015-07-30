using Abp.MultiTenancy;
using StarBlogs.Users;

namespace StarBlogs.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {
        protected Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}