using System.Linq;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using EntityFramework.DynamicFilters;
using StarBlogs.Authorization;
using StarBlogs.EntityFramework;
using StarBlogs.MultiTenancy;
using StarBlogs.Stars;
using StarBlogs.Users;
using StarBlogs.Blogs;
using System;

namespace StarBlogs.Migrations.Data
{
    public class InitialDataBuilder
    {
        private User adminUserForDefaultTenant;
        public void Build(StarBlogsDbContext context)
        {
            context.DisableAllFilters();
            CreateUserAndRoles(context);
            CreateStarTag(context);
            CreateStarBlogAndPostPic(context);
        }
        
        private void CreateStarBlogAndPostPic(StarBlogsDbContext context)
        {
            var star = context.Stars.FirstOrDefault(r => r.Name == "Disneyland Resort");
            if (star == null)
            {
                star = context.Stars.Add(new Star
                {
                    Name = "Disneyland Resort",
                    ChineseName = "迪士尼乐园",
                    Nickname = "",
                    CreatorUserId = adminUserForDefaultTenant.Id,
                    Description = "The official Twitter feed for the Disneyland Resort – Celebrating #Disneyland60 and tweeting “Live from 1955” today! #LiveFrom55",
                    Gender = Helpers.Gender.Undefined,
                    CreationTime = DateTime.Now,
                    IsBlocked = false,
                    LastUpdateTime = DateTime.Now

                });
                context.SaveChanges();
            }
            var blog = context.Blogs.FirstOrDefault(r => r.StarId == star.Id && r.ProviderName == "twitter");
            if (blog == null)
            {
                blog = context.Blogs.Add(new Blog
                {
                    CreationTime = DateTime.Now,
                    Description = "",
                    LastUpdateTime = DateTime.Now,
                    Name = "Disneyland",
                    Provider = BlogProvider.TWITTER,
                    StarId = star.Id,
                    ProviderName = "twitter",
                    Url = "https://twitter.com/Disneyland"
                });
                context.SaveChanges();
            }
            
            var post = context.OriginalPosts.FirstOrDefault(r => r.BlogId == blog.Id);
            if (post == null)
            {
                post = context.OriginalPosts.Add(new OriginalPost
                {
                    BlogId = blog.Id,
                    Content = "Visiting Disneyland Resort this summer? Have fun in the sun with these merchandise picks: http://di.sn/6011BEtU5  ",
                    PostTime = DateTime.Now,
                    StarId = star.Id,
                    DefaultTranslate = "前往迪斯尼乐园度假村今年夏天吗?在与这些商品拿太阳玩得开心: http://di.sn/6011BEtU5  "
                });
                context.OriginalPosts.Add(new OriginalPost
                {
                    BlogId = blog.Id,
                    Content = "It's a beautiful morning in Disneyland park! How many days until your next visit? #Disneyland60 ",
                    PostTime = DateTime.Now,
                    StarId = star.Id,
                    DefaultTranslate = "在迪斯尼乐园，是一个美丽的早晨!直到您下次的多少天? #Disneyland60"
                });
                OriginalPost post2 = context.OriginalPosts.Add(new OriginalPost
                {
                    BlogId = blog.Id,
                    Content = "Luigi’s Rollickin’ Roadsters opens next year in #CarsLand! Here's a look at the attraction: http://di.sn/6014BEW3k  ",
                    PostTime = DateTime.Now,
                    StarId = star.Id,
                    DefaultTranslate = "笑洛佩兹跑车打开#CarsLand明年!看一看的景点: http://di.sn/6014BEW3k  "
                });
                context.SaveChanges();
                var pic = context.Pictures.FirstOrDefault(r => r.PostId == post.Id);
                if (pic == null)
                {
                    pic = context.Pictures.Add(new Picture
                    {
                        PostId = post.Id,
                        StarId = star.Id,
                        BlogId = blog.Id,
                        Url = "https://pbs.twimg.com/media/CLBePeGUkAI2wtx.jpg"
                    });
                    context.Pictures.Add(new Picture
                    {
                        PostId = post.Id,
                        StarId = star.Id,
                        BlogId = blog.Id,
                        Url = "https://pbs.twimg.com/media/CLBeP0RVEAAQoPi.jpg"
                    });
                    context.Pictures.Add(new Picture
                    {
                        PostId = post.Id,
                        StarId = star.Id,
                        BlogId = blog.Id,
                        Url = "https://pbs.twimg.com/media/CLBeQHSVAAAOxdW.jpg"
                    });
                    context.Pictures.Add(new Picture
                    {
                        PostId = post.Id,
                        StarId = star.Id,
                        BlogId = blog.Id,
                        Url = "https://pbs.twimg.com/media/CLBeQQ-UwAAjDgS.jpg"
                    });
                    context.Pictures.Add(new Picture
                    {
                        PostId = post2.Id,
                        StarId = star.Id,
                        BlogId = blog.Id,
                        Url = "https://pbs.twimg.com/media/CK9dkcDWoAAJlHt.jpg"
                    });
                    context.SaveChanges();
                }
            }
            
            
        }

        private void CreateUserAndRoles(StarBlogsDbContext context)
        {
            #region 创建null租户的角色“Administrator”

            var adminRoleForTenancyOwner = context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == "Administrator");
            if (adminRoleForTenancyOwner == null)
            {
                //注意此处租户ID为null
                adminRoleForTenancyOwner = context.Roles.Add(new Role(null, "Administrator", "系统管理员"));
                context.SaveChanges();
            }
            #endregion
            
            #region 创建null租户的用户“Admin”并赋予其Administrator角色
            var adminUserForTenancyOwner = context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == "admin");
            if (adminUserForTenancyOwner == null)
            {
                adminUserForTenancyOwner = context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = "admin",
                        Name = "系统",
                        Surname = "管理员",
                        EmailAddress = "sagacite@163.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });

                context.SaveChanges();

                //增加admin用户和Administrator角色的对应关系，也就是使用户“admin”赋予Administrator角色。
                context.UserRoles.Add(new UserRole(adminUserForTenancyOwner.Id, adminRoleForTenancyOwner.Id));

                context.SaveChanges();
            }
            #endregion

            #region 创建租户"Default"

            var defaultTenant = context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                defaultTenant = context.Tenants.Add(new Tenant("Default", "Default"));
                context.SaveChanges();
            }
            #endregion

            #region 创建Default租户的角色"Administrator"
            //获得租户对象后，就可以根据租户ID创建属于该租户的角色，此处角色为Administrator
            var adminRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "Admin");
            if (adminRoleForDefaultTenant == null)
            {
                adminRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "Administrator", "系统管理员"));
                context.SaveChanges();

                //默认租户下Administrator角色的权限设置
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = PermissionNames.CanManageStars, IsGranted = true });

                context.SaveChanges();
            }
            #endregion

            #region 创建Default租户的角色"TranslateManager"
            var userRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "TranslateManager");
            if (userRoleForDefaultTenant == null)
            {
                userRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "TranslateManager", "翻译主管"));
                context.SaveChanges();

                //Permission definitions for StarBlogs of 'Default' tenant
                context.Permissions.Add(new RolePermissionSetting { RoleId = userRoleForDefaultTenant.Id, Name = PermissionNames.CanManageTranslatings, IsGranted = true });
                context.SaveChanges();
            }
            #endregion

            #region 创建Default租户下的用户“admin”，并赋予Administrator、TranslateManager两个角色
            adminUserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "admin");
            if (adminUserForDefaultTenant == null)
            {
                adminUserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "admin",
                        Name = "系统",
                        Surname = "管理员",
                        EmailAddress = "sagacite@163.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

            }
            #endregion

            #region 创建用户“ouyang”，并赋予TranslateManager角色
            var StarBlogsForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "ouyang");
            if (StarBlogsForDefaultTenant == null)
            {
                StarBlogsForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "ouyang",
                        Name = "Guangcong",
                        Surname = "Ouyang",
                        EmailAddress = "sagacite@163.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(StarBlogsForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

            }
            #endregion

        }
        private void CreateStarTag(StarBlogsDbContext context)
        {
            #region 创建初始的StarCategoryTags：明星归类标签
            addStarTagSetting(context, "影视");
            addStarTagSetting(context, "音乐");
            addStarTagSetting(context, "体育");
            addStarTagSetting(context, "歌手");
            addStarTagSetting(context, "官方");
            context.SaveChanges();
            #endregion
        }
        private void addStarTagSetting(StarBlogsDbContext context, string tagName)
        {
            var tag = context.StarTagSettings.FirstOrDefault<StarTagSetting>(t => t.TagName == tagName);
            if (tag == null)
            {
                tag = context.StarTagSettings.Add(
                    new StarTagSetting
                    {
                        TagName = tagName,
                        ParentTagId = 0
                    });
            }
        }
    }
}