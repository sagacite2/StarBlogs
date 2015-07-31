namespace StarBlogs.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class FixstarTag3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StarTag", "TagId", "dbo.StarTag");
            AlterTableAnnotations(
                "dbo.StarTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StarId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_StarTag_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddForeignKey("dbo.StarTag", "TagId", "dbo.StarTagSetting", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarTag", "TagId", "dbo.StarTagSetting");
            AlterTableAnnotations(
                "dbo.StarTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StarId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_StarTag_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AddForeignKey("dbo.StarTag", "TagId", "dbo.StarTag", "Id");
        }
    }
}
