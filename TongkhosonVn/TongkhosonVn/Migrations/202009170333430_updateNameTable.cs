namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNameTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategories", newName: "Table_ProductCategory");
            RenameTable(name: "dbo.PostCategories", newName: "Table_PostCategory");
            AddColumn("dbo.FeedBacks", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedBacks", "Avatar");
            RenameTable(name: "dbo.Table_PostCategory", newName: "PostCategories");
            RenameTable(name: "dbo.Table_ProductCategory", newName: "ProductCategories");
        }
    }
}
