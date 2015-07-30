namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlockStarColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Star", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Star", "IsBlocked");
        }
    }
}
