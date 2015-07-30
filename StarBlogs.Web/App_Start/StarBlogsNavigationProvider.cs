using Abp.Application.Navigation;
using Abp.Localization;
using StarBlogs.Authorization;

namespace StarBlogs.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class StarBlogsNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", StarBlogsConsts.LocalizationSourceName),
                        url: "/",
                        icon: ""
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Manage",
                        new LocalizableString("Manage", StarBlogsConsts.LocalizationSourceName),
                        url: "/Manage",
                        icon: ""
                        ).AddItem(
                            new MenuItemDefinition(
                                "ManageStar",
                                new LocalizableString("ManageStar", StarBlogsConsts.LocalizationSourceName),
                                icon: "",
                                url: "/Manage/Index",
                                requiresAuthentication: true,
                                requiredPermissionName:PermissionNames.CanManageStars
                                )
                        ).AddItem(
                            new MenuItemDefinition(
                                "ManageTranslate",
                                new LocalizableString("ManageTranslate", StarBlogsConsts.LocalizationSourceName),
                                icon: "",
                                url: "/Manage/Translate",
                                requiresAuthentication: true,
                                requiredPermissionName: PermissionNames.CanManageTranslatings
                                )
                        )
                );
        }
    }
}
