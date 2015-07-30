using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization.Users;
using Abp.UI;
using Abp.Web.Mvc.Models;
using Microsoft.AspNet.Identity;
using StarBlogs.Web.Models.Account;
using StarBlogs.Users;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace StarBlogs.Web.Controllers
{
    public class AccountController : StarBlogsControllerBase
    {
        private readonly UserManager _userManager;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public AccountController(UserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("你提交的表单有误！");
            }

            var loginResult = await _userManager.LoginAsync(
                loginModel.Username,
                loginModel.Password,
                loginModel.TenancyName
                );

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    break;
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    throw new UserFriendlyException("用户名或密码不正确！");
                case AbpLoginResultType.InvalidTenancyName:
                    throw new UserFriendlyException("无此租户: " + loginModel.TenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    throw new UserFriendlyException("租户未激活: " + loginModel.TenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    throw new UserFriendlyException("用户未激活: " + loginModel.Username);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    throw new UserFriendlyException("Email地址未认证！");
                default: //Can not fall to default for now. But other result types can be added in the future and we may forget to handle it
                    throw new UserFriendlyException("未知登录错误: " + loginResult.Result);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = loginModel.RememberMe }, loginResult.Identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return Json(new MvcAjaxResponse { TargetUrl = returnUrl });
        }

        public ActionResult Logout(string returnUrl = "")
        {
            AuthenticationManager.SignOut();
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            return Redirect(returnUrl);
        }
        [ChildActionOnly]
        public PartialViewResult SimpleLogin()
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
            {
                var model = new WelcomeModel();
                model.Username = AuthenticationManager.User.Identity.Name;
                return PartialView("_SimpleWelcome",model);
            }else
                return PartialView("_SimpleLogin");
        }
    }
}