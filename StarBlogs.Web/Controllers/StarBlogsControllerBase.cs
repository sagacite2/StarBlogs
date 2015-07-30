using Abp.Web.Mvc.Controllers;

namespace StarBlogs.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class StarBlogsControllerBase : AbpController
    {
        protected StarBlogsControllerBase()
        {
            LocalizationSourceName = StarBlogsConsts.LocalizationSourceName;
        }
    }
}