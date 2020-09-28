namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefortableProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Warranty", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "WarrantyPolicy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "WarrantyPolicy", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Products", "Warranty");
        }
    }
}
