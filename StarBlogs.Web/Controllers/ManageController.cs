using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarBlogs.Stars;
using StarBlogs.Blogs;
using StarBlogs.Stars.Dtos;
using Abp.UI;

namespace StarBlogs.Web.Controllers
{
    public class ManageController : StarBlogsControllerBase
    {
        IStarAppService _starService;
        public ManageController(IStarAppService starService)
        {
            _starService = starService;
        }
        public ActionResult Index(int page=1)
        {
            var stars = _starService.GetStars(
                    new GetStarsInput()
                    {
                        MaxResultCount = 10,
                        SkipCount = 0,
                        Sorting = "CreationTime Desc"
                    }
                );
            return View(stars);
        }
        [HttpPost]
        public ActionResult CreateEditStar(CreateUpdateStarInput input)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("你提交的表单有误！");
            }
            _starService.CreateStar(input);
            return RedirectToAction("Index");
        }
        public ActionResult Translate()
        {
            return View();
        }
    }
}