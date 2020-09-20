namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableTypicalCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypicalCustomers", "FullName", c => c.String(maxLength: 100));
            DropColumn("dbo.TypicalCustomers", "Subject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TypicalCustomers", "Subject", c => c.String(maxLength: 100));
            DropColumn("dbo.TypicalCustomers", "FullName");
        }
    }
}
