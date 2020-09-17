namespace WebPanit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initalDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerName = c.String(nullable: false, maxLength: 100),
                        Image = c.String(maxLength: 500),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Location = c.Int(nullable: false),
                        Url = c.String(maxLength: 500),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConfigSites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Facebook = c.String(maxLength: 500),
                        Youtube = c.String(maxLength: 500),
                        Instagram = c.String(maxLength: 500),
                        Twitter = c.String(maxLength: 500),
                        GooglePlus = c.String(maxLength: 500),
                        Address = c.String(maxLength: 500),
                        PhoneNumber = c.String(maxLength: 15),
                        Email = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                        LogoTop = c.String(maxLength: 500),
                        LogoBottom = c.String(maxLength: 500),
                        About = c.String(storeType: "ntext"),
                        Services = c.String(storeType: "ntext"),
                        PrivacyPolicy = c.String(storeType: "ntext"),
                        Payment = c.String(storeType: "ntext"),
                        Contact = c.String(storeType: "ntext"),
                        Helper = c.String(storeType: "ntext"),
                        Transport = c.String(storeType: "ntext"),
                        Conditions = c.String(storeType: "ntext"),
                        RefundPolicy = c.String(storeType: "ntext"),
                        PromotionPolicy = c.String(storeType: "ntext"),
                        ConstructionRecords = c.String(storeType: "ntext"),
                        AgencyPolicy = c.String(storeType: "ntext"),
                        CapacityProfile = c.String(storeType: "ntext"),
                        TypicalCustomers = c.String(storeType: "ntext"),
                        CopyRight = c.String(storeType: "ntext"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FullNname = c.String(),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Description = c.String(nullable: false, maxLength: 500),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        FlagHome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 50),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MemberID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false, storeType: "date"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShipDate = c.DateTime(nullable: false, storeType: "date"),
                        Status = c.Boolean(nullable: false),
                        Remove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.MemberID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.MemberID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 20),
                        Images = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                        Detail = c.String(nullable: false, storeType: "ntext"),
                        WarrantyPolicy = c.String(storeType: "ntext"),
                        CreateDate = c.DateTime(nullable: false, storeType: "date"),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        FlagHome = c.Boolean(nullable: false),
                        View = c.Int(nullable: false),
                        ProductCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryID, cascadeDelete: true)
                .Index(t => t.ProductCategoryID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        DisplayOrder = c.Int(nullable: false),
                        Logo = c.String(maxLength: 250),
                        CreateDate = c.DateTime(nullable: false, storeType: "date"),
                        Active = c.Boolean(nullable: false),
                        ShowMenuVertical = c.Boolean(nullable: false),
                        FlagHome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagProductCategories",
                c => new
                    {
                        TradeMarkID = c.Int(nullable: false),
                        ProductCategoryID = c.Int(nullable: false),
                        TagName = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TradeMarkID, t.ProductCategoryID })
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.TradeMarks", t => t.TradeMarkID, cascadeDelete: true)
                .Index(t => t.TradeMarkID)
                .Index(t => t.ProductCategoryID);
            
            CreateTable(
                "dbo.TradeMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Avatar = c.String(maxLength: 250),
                        Active = c.Boolean(nullable: false),
                        FlagHome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        Content = c.String(nullable: false),
                        Image = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        DisPlayOrder = c.Int(nullable: false),
                        View = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        FlagHome = c.Boolean(nullable: false),
                        PostCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostCategories", t => t.PostCategoryID, cascadeDelete: true)
                .Index(t => t.PostCategoryID);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        Url = c.String(maxLength: 500),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ShowOnHome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PassWord = c.String(),
                        RoleID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostCategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.TagProductCategories", "TradeMarkID", "dbo.TradeMarks");
            DropForeignKey("dbo.TagProductCategories", "ProductCategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.Orders", "MemberID", "dbo.Members");
            DropIndex("dbo.Posts", new[] { "PostCategoryID" });
            DropIndex("dbo.TagProductCategories", new[] { "ProductCategoryID" });
            DropIndex("dbo.TagProductCategories", new[] { "TradeMarkID" });
            DropIndex("dbo.Products", new[] { "ProductCategoryID" });
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "MemberID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Posts");
            DropTable("dbo.TradeMarks");
            DropTable("dbo.TagProductCategories");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Members");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.ConfigSites");
            DropTable("dbo.Banners");
        }
    }
}
