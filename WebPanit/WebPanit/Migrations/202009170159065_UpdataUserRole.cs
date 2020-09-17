namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataUserRole : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRoles", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserRoles", "PassWord", c => c.String(nullable: false, maxLength: 120));
            DropColumn("dbo.UserRoles", "RoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "RoleID", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRoles", "PassWord", c => c.String());
            AlterColumn("dbo.UserRoles", "UserName", c => c.String());
        }
    }
}
