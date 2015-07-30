using Abp.Application.Services;

namespace StarBlogs
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class StarBlogsAppServiceBase : ApplicationService
    {
        protected StarBlogsAppServiceBase()
        {
            LocalizationSourceName = StarBlogsConsts.LocalizationSourceName;
        }
    }
}