namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableReceiveEmail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReceiveEmails", "Email", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReceiveEmails", "Email", c => c.String(maxLength: 100));
        }
    }
}
