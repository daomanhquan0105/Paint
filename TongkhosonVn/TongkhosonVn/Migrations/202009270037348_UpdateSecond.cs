namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSecond : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Url", c => c.String(maxLength: 500));
            AddColumn("dbo.Products", "MetaTitle", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.Table_ProductCategory", "ParentProductCategoryId", c => c.Int());
            AddColumn("dbo.Table_ProductCategory", "Url", c => c.String(maxLength: 500));
            AddColumn("dbo.Table_ProductCategory", "MetaTitle", c => c.String(maxLength: 100));
            AddColumn("dbo.Table_ProductCategory", "MetaDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.TradeMarks", "Body", c => c.String());
            AddColumn("dbo.TradeMarks", "Url", c => c.String(maxLength: 500));
            AddColumn("dbo.TradeMarks", "MetaTitle", c => c.String(maxLength: 100));
            AddColumn("dbo.TradeMarks", "MetaDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.Posts", "Url", c => c.String(maxLength: 500));
            AddColumn("dbo.Posts", "MetaTitle", c => c.String(maxLength: 100));
            AddColumn("dbo.Posts", "MetaDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.Table_PostCategory", "MetaTitle", c => c.String(maxLength: 100));
            AddColumn("dbo.Table_PostCategory", "MetaDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "Detail", c => c.String(nullable: false));
            CreateIndex("dbo.Table_ProductCategory", "ParentProductCategoryId");
            AddForeignKey("dbo.Table_ProductCategory", "ParentProductCategoryId", "dbo.Table_ProductCategory", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Table_ProductCategory", "ParentProductCategoryId", "dbo.Table_ProductCategory");
            DropIndex("dbo.Table_ProductCategory", new[] { "ParentProductCategoryId" });
            AlterColumn("dbo.Products", "Detail", c => c.String(nullable: false, storeType: "ntext"));
            DropColumn("dbo.Table_PostCategory", "MetaDescription");
            DropColumn("dbo.Table_PostCategory", "MetaTitle");
            DropColumn("dbo.Posts", "MetaDescription");
            DropColumn("dbo.Posts", "MetaTitle");
            DropColumn("dbo.Posts", "Url");
            DropColumn("dbo.TradeMarks", "MetaDescription");
            DropColumn("dbo.TradeMarks", "MetaTitle");
            DropColumn("dbo.TradeMarks", "Url");
            DropColumn("dbo.TradeMarks", "Body");
            DropColumn("dbo.Table_ProductCategory", "MetaDescription");
            DropColumn("dbo.Table_ProductCategory", "MetaTitle");
            DropColumn("dbo.Table_ProductCategory", "Url");
            DropColumn("dbo.Table_ProductCategory", "ParentProductCategoryId");
            DropColumn("dbo.Products", "MetaDescription");
            DropColumn("dbo.Products", "MetaTitle");
            DropColumn("dbo.Products", "Url");
        }
    }
}
