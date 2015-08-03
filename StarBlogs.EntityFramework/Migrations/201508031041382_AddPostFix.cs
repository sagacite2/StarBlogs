namespace StarBlogs.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostFix : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.OriginalPost", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OriginalPost", "IsBlocked");
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
            
        }
    }
}
