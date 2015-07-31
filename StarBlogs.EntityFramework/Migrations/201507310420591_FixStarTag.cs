namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixStarTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StarTagSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        ParentTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.StarCategoryTag");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StarCategoryTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.StarTagSetting");
        }
    }
}
