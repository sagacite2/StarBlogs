using System.Reflection;
using Abp.Modules;

namespace StarBlogs
{
    public class StarBlogsCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
