namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataTableOrder3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderCode", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderCode", c => c.String());
        }
    }
}
