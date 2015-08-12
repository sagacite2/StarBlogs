using Abp.Authorization.Users;
using StarBlogs.MultiTenancy;

namespace StarBlogs.Users
{
    public class User : AbpUser<Tenant, User>
    {
       
        public override string ToString()
        {
            return string.Format("[User {0}] {1}", Id, UserName);
        }
    }
}