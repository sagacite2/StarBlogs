using System.Reflection;
using Abp.Modules;
using StarBlogs.Configuration;
using StarBlogs.Authorization;

namespace StarBlogs
{
    [DependsOn(typeof(StarBlogsCoreModule))]
    public class StarBlogsApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Authorization.Providers.Add<StarAuthorizationProvider>();
            Configuration.Settings.Providers.Add<MySettingProvider>();
        }
    }
}
