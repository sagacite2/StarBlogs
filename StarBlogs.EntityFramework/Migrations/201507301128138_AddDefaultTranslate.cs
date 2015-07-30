namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultTranslate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Translation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginalPostId = c.Int(nullable: false),
                        StarId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                        Content = c.String(),
                        Vote = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.OriginalPost", t => t.OriginalPostId, cascadeDelete: true)
                .Index(t => t.OriginalPostId)
                .Index(t => t.CreatorUserId);
            
            AddColumn("dbo.OriginalPost", "DefaultTranslate", c => c.String());
            AddColumn("dbo.Picture", "BlogId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Translation", "OriginalPostId", "dbo.OriginalPost");
            DropForeignKey("dbo.Translation", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.Translation", new[] { "CreatorUserId" });
            DropIndex("dbo.Translation", new[] { "OriginalPostId" });
            DropColumn("dbo.Picture", "BlogId");
            DropColumn("dbo.OriginalPost", "DefaultTranslate");
            DropTable("dbo.Translation");
        }
    }
}
