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
            //这里的字符串app，影响到在js中使用约定的服务，如abp.services.app.star
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(StarBlogsApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
