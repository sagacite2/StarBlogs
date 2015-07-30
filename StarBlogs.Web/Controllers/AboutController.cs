using System.Web.Mvc;

namespace StarBlogs.Web.Controllers
{
    public class AboutController : StarBlogsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}