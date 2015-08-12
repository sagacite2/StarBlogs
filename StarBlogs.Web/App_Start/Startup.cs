using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using StarBlogs.Web;
using Owin;
[assembly: OwinStartup(typeof(Startup))]

namespace StarBlogs.Web
{
    public class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void Configuration(IAppBuilder app)
        {
            /*IAppBuilder的扩展方法UseCookieAuthentication()，是在Microsoft.Owin.Security.Cookies.dll上定义的，其源码为：
            
              public static IAppBuilder UseCookieAuthentication(this IAppBuilder app,
                CookieAuthenticationOptions options)
                {
                    if (app == null)
                    {
                        throw new ArgumentNullException("app");
                    }
                    app.Use(typeof(CookieAuthenticationMiddleware), app, options);//添加OWin middleware 组件到 OWin 管道
                    app.UseStageMarker(PipelineStage.Authenticate);//为前面添加的middleware指定在IIS 管道的哪个阶段执行。
                    return app;
                }
             */
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")

            });//CookieAuthenticationOptions还有大量的属性可以配置，参看csdn：https://msdn.microsoft.com/en-us/library/microsoft.owin.security.cookies.cookieauthenticationoptions(v=vs.113).aspx

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            //还有可以启用的登录方式：
            //app.UseOAuthAuthorizationServer
            //app.UseOAuthBearerAuthentication
            //app.UseOAuthBearerTokens
            //app.UseTwoFactorRememberBrowserCookie
            //app.UseTwoFactorSignInCookie
            //下面所说的UseTwitterAuthentication之类的方法仍未实现，启用第三方登录的话，需要费些力气
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}