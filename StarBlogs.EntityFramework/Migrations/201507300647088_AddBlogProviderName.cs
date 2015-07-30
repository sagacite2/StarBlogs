namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogProviderName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "ProviderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "ProviderName");
        }
    }
}
