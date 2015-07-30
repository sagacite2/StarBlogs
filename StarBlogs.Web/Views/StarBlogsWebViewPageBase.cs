using Abp.Web.Mvc.Views;

namespace StarBlogs.Web.Views
{
    public abstract class StarBlogsWebViewPageBase : StarBlogsWebViewPageBase<dynamic>
    {

    }

    public abstract class StarBlogsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected StarBlogsWebViewPageBase()
        {
            LocalizationSourceName = StarBlogsConsts.LocalizationSourceName;
        }
    }
}