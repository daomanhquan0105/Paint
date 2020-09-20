namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTableLibraryImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LibraryImages", "Avatar", c => c.String(maxLength: 500));
            DropColumn("dbo.LibraryImages", "Avater");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LibraryImages", "Avater", c => c.String(maxLength: 500));
            DropColumn("dbo.LibraryImages", "Avatar");
        }
    }
}
