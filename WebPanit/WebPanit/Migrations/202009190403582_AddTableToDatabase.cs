namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 60),
                        Email = c.String(maxLength: 100),
                        Subject = c.String(maxLength: 200),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LibraryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Avater = c.String(maxLength: 500),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        FlagHome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ConfigSites", "Contact", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "Contact");
            DropTable("dbo.LibraryImages");
            DropTable("dbo.Contacts");
        }
    }
}
