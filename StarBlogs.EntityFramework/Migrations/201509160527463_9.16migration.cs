namespace StarBlogs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _916migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpUsers", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AbpUsers", "Surname", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AbpUsers", "EmailAddress", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AbpUsers", "PasswordResetCode", c => c.String(maxLength: 328));
            DropColumn("dbo.AbpUsers", "PhoneNumber");
            DropColumn("dbo.AbpUsers", "PhoneNumberConfirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbpUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AbpUsers", "PasswordResetCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.AbpUsers", "EmailAddress", c => c.String(maxLength: 256));
            AlterColumn("dbo.AbpUsers", "Surname", c => c.String(maxLength: 32));
            AlterColumn("dbo.AbpUsers", "Name", c => c.String(maxLength: 32));
        }
    }
}
