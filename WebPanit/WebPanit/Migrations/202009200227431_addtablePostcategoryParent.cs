namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablePostcategoryParent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostCategoryParents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Table_PostCategory", "ParentPostCategoryID", c => c.Int());
            CreateIndex("dbo.Table_PostCategory", "ParentPostCategoryID");
            AddForeignKey("dbo.Table_PostCategory", "ParentPostCategoryID", "dbo.PostCategoryParents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Table_PostCategory", "ParentPostCategoryID", "dbo.PostCategoryParents");
            DropIndex("dbo.Table_PostCategory", new[] { "ParentPostCategoryID" });
            DropColumn("dbo.Table_PostCategory", "ParentPostCategoryID");
            DropTable("dbo.PostCategoryParents");
        }
    }
}
