using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace StarBlogs
{
    [DependsOn(typeof(AbpWebApiModule), typeof(StarBlogsApplicationModule))]
    public class StarBlogsWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(StarBlogsApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
