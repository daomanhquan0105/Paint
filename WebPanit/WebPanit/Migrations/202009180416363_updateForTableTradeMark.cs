namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateForTableTradeMark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeMarks", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradeMarks", "DisplayOrder");
        }
    }
}
