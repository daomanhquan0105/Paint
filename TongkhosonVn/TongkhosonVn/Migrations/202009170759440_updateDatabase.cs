namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencyPolicies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CapacityProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConstructionRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrivacyPolicies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PromotionPolicies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypicalCustomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 100),
                        Avatar = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "PostType", c => c.Int(nullable: false));
            DropColumn("dbo.ConfigSites", "About");
            DropColumn("dbo.ConfigSites", "Services");
            DropColumn("dbo.ConfigSites", "PrivacyPolicy");
            DropColumn("dbo.ConfigSites", "Contact");
            DropColumn("dbo.ConfigSites", "Conditions");
            DropColumn("dbo.ConfigSites", "PromotionPolicy");
            DropColumn("dbo.ConfigSites", "ConstructionRecords");
            DropColumn("dbo.ConfigSites", "AgencyPolicy");
            DropColumn("dbo.ConfigSites", "CapacityProfile");
            DropColumn("dbo.ConfigSites", "TypicalCustomers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConfigSites", "TypicalCustomers", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "CapacityProfile", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "AgencyPolicy", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "ConstructionRecords", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "PromotionPolicy", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "Conditions", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "Contact", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "PrivacyPolicy", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "Services", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ConfigSites", "About", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Posts", "PostType");
            DropTable("dbo.TypicalCustomers");
            DropTable("dbo.Services");
            DropTable("dbo.PromotionPolicies");
            DropTable("dbo.PrivacyPolicies");
            DropTable("dbo.ConstructionRecords");
            DropTable("dbo.Conditions");
            DropTable("dbo.CapacityProfiles");
            DropTable("dbo.AgencyPolicies");
        }
    }
}
