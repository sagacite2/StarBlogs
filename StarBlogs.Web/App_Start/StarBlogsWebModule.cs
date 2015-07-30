using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Sources.Xml;
using Abp.Modules;

namespace StarBlogs.Web
{
    [DependsOn(typeof(StarBlogsDataModule), typeof(StarBlogsApplicationModule), typeof(StarBlogsWebApiModule))]
    public class StarBlogsWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england"));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn",true));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new XmlLocalizationSource(
                    StarBlogsConsts.LocalizationSourceName,
                    HttpContext.Current.Server.MapPath("~/Localization/StarBlogs")
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<StarBlogsNavigationProvider>();
            //启用第二个导航
            //Configuration.Navigation.Providers.Add<OtherNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
