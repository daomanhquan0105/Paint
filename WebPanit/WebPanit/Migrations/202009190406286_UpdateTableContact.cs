namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(maxLength: 100));
            DropColumn("dbo.Contacts", "PhoneNumber");
        }
    }
}
