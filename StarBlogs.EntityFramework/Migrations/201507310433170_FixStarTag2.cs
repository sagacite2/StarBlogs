namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixStarTag2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StarTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StarId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Star", t => t.StarId, cascadeDelete: true)
                .ForeignKey("dbo.StarTag", t => t.TagId)
                .Index(t => t.StarId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarTag", "TagId", "dbo.StarTag");
            DropForeignKey("dbo.StarTag", "StarId", "dbo.Star");
            DropIndex("dbo.StarTag", new[] { "TagId" });
            DropIndex("dbo.StarTag", new[] { "StarId" });
            DropTable("dbo.StarTag");
        }
    }
}
