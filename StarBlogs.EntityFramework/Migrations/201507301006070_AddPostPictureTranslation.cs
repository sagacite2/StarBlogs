namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostPictureTranslation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OriginalPost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        StarId = c.Int(nullable: false),
                        Content = c.String(),
                        PostTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Picture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        StarId = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OriginalPost", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Picture", "PostId", "dbo.OriginalPost");
            DropForeignKey("dbo.OriginalPost", "BlogId", "dbo.Blog");
            DropIndex("dbo.Picture", new[] { "PostId" });
            DropIndex("dbo.OriginalPost", new[] { "BlogId" });
            DropTable("dbo.Picture");
            DropTable("dbo.OriginalPost");
        }
    }
}
