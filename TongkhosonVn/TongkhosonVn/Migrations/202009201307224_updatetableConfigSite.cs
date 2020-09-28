namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableConfigSite : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConfigSites", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConfigSites", "Password", c => c.String(maxLength: 100));
        }
    }
}
