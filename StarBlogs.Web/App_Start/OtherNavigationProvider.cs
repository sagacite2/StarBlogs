using Abp.Application.Navigation;
using Abp.Localization;
using StarBlogs.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBlogs.Web
{
    /// <summary>
    /// 如有需要，可在StarBlogsWebModule中启用这个导航
    /// </summary>
    public class OtherNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Other1",
                        new LocalizableString("Other1", StarBlogsConsts.LocalizationSourceName),
                        url: "/Other1",
                        icon: ""
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Other2",
                        new LocalizableString("Other2", StarBlogsConsts.LocalizationSourceName),
                        url: "/Other2",
                        icon: ""
                        ).AddItem(
                            new MenuItemDefinition(
                                "Other3",
                                new LocalizableString("Other3", StarBlogsConsts.LocalizationSourceName),
                                icon: "",
                                url: "/Other3"

                                )
                        ).AddItem(
                            new MenuItemDefinition(
                                "Other4",
                                new LocalizableString("Other4", StarBlogsConsts.LocalizationSourceName),
                                icon: "",
                                url: "/Other4"

                                )
                        )
                );
        }
    }
}