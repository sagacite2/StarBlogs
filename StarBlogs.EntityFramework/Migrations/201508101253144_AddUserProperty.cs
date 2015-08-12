namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AbpUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Translation", "VoteUp", c => c.Int(nullable: false));
            AddColumn("dbo.Translation", "VoteDown", c => c.Int(nullable: false));
            AddColumn("dbo.Translation", "InheritedEditionId", c => c.Int(nullable: false));
            AddColumn("dbo.Translation", "CommentCount", c => c.Int(nullable: false));
            DropColumn("dbo.Translation", "Vote");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Translation", "Vote", c => c.Int(nullable: false));
            DropColumn("dbo.Translation", "CommentCount");
            DropColumn("dbo.Translation", "InheritedEditionId");
            DropColumn("dbo.Translation", "VoteDown");
            DropColumn("dbo.Translation", "VoteUp");
            DropColumn("dbo.AbpUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AbpUsers", "PhoneNumber");
        }
    }
}
