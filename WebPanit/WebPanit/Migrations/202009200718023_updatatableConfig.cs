namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatatableConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "PricePaint", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "PricePaint");
        }
    }
}
