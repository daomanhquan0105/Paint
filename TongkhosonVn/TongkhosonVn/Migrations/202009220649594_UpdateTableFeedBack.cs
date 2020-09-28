namespace TongkhosonVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFeedBack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedBacks", "NoteContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedBacks", "NoteContent");
        }
    }
}
