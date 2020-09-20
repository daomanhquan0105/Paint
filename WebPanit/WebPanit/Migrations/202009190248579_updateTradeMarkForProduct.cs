namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTradeMarkForProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TradeMarkID", c => c.Int());
            CreateIndex("dbo.Products", "TradeMarkID");
            AddForeignKey("dbo.Products", "TradeMarkID", "dbo.TradeMarks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "TradeMarkID", "dbo.TradeMarks");
            DropIndex("dbo.Products", new[] { "TradeMarkID" });
            DropColumn("dbo.Products", "TradeMarkID");
        }
    }
}
