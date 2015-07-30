using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using StarBlogs.EntityFramework;

namespace StarBlogs
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(StarBlogsCoreModule))]
    public class StarBlogsDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "StarBlogs";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<StarBlogsDbContext>(null);
        }
    }
}
