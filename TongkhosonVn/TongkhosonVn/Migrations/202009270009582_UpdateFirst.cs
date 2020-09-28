namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFirst : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Table_PostCategory", "ParentPostCategoryID", "dbo.PostCategoryParents");
            DropIndex("dbo.Table_PostCategory", new[] { "ParentPostCategoryID" });
            AddColumn("dbo.ConfigSites", "Title", c => c.String(maxLength: 100));
            AddColumn("dbo.ConfigSites", "Description", c => c.String(maxLength: 500));
            AddColumn("dbo.ConfigSites", "HeaderCode", c => c.String());
            AddColumn("dbo.ConfigSites", "FooterCode", c => c.String());
            AddColumn("dbo.ConfigSites", "GoogleMaps", c => c.String());
            AddColumn("dbo.LibraryImages", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Table_PostCategory", "ShowMenu", c => c.Boolean(nullable: false));
            AddColumn("dbo.Table_PostCategory", "ParentCategoryId", c => c.Int());
            AddColumn("dbo.Table_PostCategory", "TypeCategory", c => c.Int(nullable: false));
            AlterColumn("dbo.ConfigSites", "Payment", c => c.String());
            AlterColumn("dbo.ConfigSites", "Contact", c => c.String());
            AlterColumn("dbo.ConfigSites", "Helper", c => c.String());
            AlterColumn("dbo.ConfigSites", "Transport", c => c.String());
            AlterColumn("dbo.ConfigSites", "PricePaint", c => c.String());
            AlterColumn("dbo.ConfigSites", "RefundPolicy", c => c.String());
            AlterColumn("dbo.ConfigSites", "CopyRight", c => c.String());
            AlterColumn("dbo.LibraryImages", "Avatar", c => c.String());
            CreateIndex("dbo.Table_PostCategory", "ParentCategoryId");
            AddForeignKey("dbo.Table_PostCategory", "ParentCategoryId", "dbo.Table_PostCategory", "Id");
            DropColumn("dbo.ConfigSites", "GooglePlus");
            DropColumn("dbo.Table_PostCategory", "ShowOnHome");
            DropColumn("dbo.Table_PostCategory", "ParentPostCategoryID");
            DropTable("dbo.PostCategoryParents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostCategoryParents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Table_PostCategory", "ParentPostCategoryID", c => c.Int());
            AddColumn("dbo.Table_PostCategory", "ShowOnHome", c => c.Boolean(nullable: false));
            AddColumn("dbo.ConfigSites", "GooglePlus", c => c.String(maxLength: 500));
            DropForeignKey("dbo.Table_PostCategory", "ParentCategoryId", "dbo.Table_PostCategory");
            DropIndex("dbo.Table_PostCategory", new[] { "ParentCategoryId" });
            AlterColumn("dbo.LibraryImages", "Avatar", c => c.String(maxLength: 500));
            AlterColumn("dbo.ConfigSites", "CopyRight", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.ConfigSites", "RefundPolicy", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.ConfigSites", "PricePaint", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.ConfigSites", "Transport", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.ConfigSites", "Helper", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.ConfigSites", "Contact", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.ConfigSites", "Payment", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Table_PostCategory", "TypeCategory");
            DropColumn("dbo.Table_PostCategory", "ParentCategoryId");
            DropColumn("dbo.Table_PostCategory", "ShowMenu");
            DropColumn("dbo.LibraryImages", "Name");
            DropColumn("dbo.ConfigSites", "GoogleMaps");
            DropColumn("dbo.ConfigSites", "FooterCode");
            DropColumn("dbo.ConfigSites", "HeaderCode");
            DropColumn("dbo.ConfigSites", "Description");
            DropColumn("dbo.ConfigSites", "Title");
            CreateIndex("dbo.Table_PostCategory", "ParentPostCategoryID");
            AddForeignKey("dbo.Table_PostCategory", "ParentPostCategoryID", "dbo.PostCategoryParents", "Id");
        }
    }
}
