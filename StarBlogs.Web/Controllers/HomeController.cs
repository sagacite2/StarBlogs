using System.Web.Mvc;

namespace StarBlogs.Web.Controllers
{
    public class HomeController : StarBlogsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}