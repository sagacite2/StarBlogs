using StarBlogs.Migrations.Data;
using System.Data.Entity.Migrations;

namespace StarBlogs.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StarBlogs.EntityFramework.StarBlogsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "StarBlogs";
        }

        protected override void Seed(StarBlogs.EntityFramework.StarBlogsDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
