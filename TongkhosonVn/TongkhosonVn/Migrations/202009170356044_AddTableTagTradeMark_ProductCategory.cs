namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTagTradeMark_ProductCategory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TagProductCategories", newName: "Tag_ProductCategry_TradeMarks");
            DropColumn("dbo.Table_ProductCategory", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Table_ProductCategory", "Logo", c => c.String(maxLength: 250));
            RenameTable(name: "dbo.Tag_ProductCategry_TradeMarks", newName: "TagProductCategories");
        }
    }
}
