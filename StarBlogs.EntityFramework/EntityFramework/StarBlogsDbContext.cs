using Abp.EntityFramework;
using Abp.Zero.EntityFramework;
using StarBlogs.Authorization;
using StarBlogs.Blogs;
using StarBlogs.MultiTenancy;
using StarBlogs.Stars;
using StarBlogs.Users;
using System.Data.Common;
using System.Data.Entity;

namespace StarBlogs.EntityFramework
{
    public class StarBlogsDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<Star> Stars { get; set; }

        public virtual IDbSet<Blog> Blogs { get; set; }
        public virtual IDbSet<Translation> Translations { get; set; }
        public virtual IDbSet<OriginalPost> OriginalPosts { get; set; }
        public virtual IDbSet<Picture> Pictures { get; set; }
        public virtual IDbSet<StarCategoryTag> StarCategoryTags { get; set; }
        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public StarBlogsDbContext()
            : base("StarBlogs")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in StarBlogsDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of StarBlogsDbContext since ABP automatically handles it.
         */
        public StarBlogsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        public StarBlogsDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
