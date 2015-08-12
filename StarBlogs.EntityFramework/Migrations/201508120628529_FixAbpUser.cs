namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixAbpUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpUsers", "Name", c => c.String(maxLength: 32));
            AlterColumn("dbo.AbpUsers", "Surname", c => c.String(maxLength: 32));
            AlterColumn("dbo.AbpUsers", "EmailAddress", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpUsers", "EmailAddress", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AbpUsers", "Surname", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AbpUsers", "Name", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
