namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        BuildingNumber = c.String(),
                        ApartametnNumber = c.String(),
                        PostalCode = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Date_of_birth = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        AddressID = c.Int(nullable: false),
                        DeliveryMethodID = c.Int(nullable: false),
                        PaymentMethodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("dbo.DeliveryMethods", t => t.DeliveryMethodID, cascadeDelete: true)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.AddressID)
                .Index(t => t.DeliveryMethodID)
                .Index(t => t.PaymentMethodID);
            
            CreateTable(
                "dbo.DeliveryMethods",
                c => new
                    {
                        DeliveryMethodID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryMethodID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        PaymentMethodID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMethodID);
            
            CreateTable(
                "dbo.Product_Order",
                c => new
                    {
                        Product_OrderID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_OrderID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Visibility = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Add_date = c.DateTime(nullable: false),
                        Sold_units = c.Int(nullable: false),
                        TaxID = c.Int(),
                        CategoryID = c.Int(nullable: false),
                        ProducerID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerID)
                .ForeignKey("dbo.Taxes", t => t.TaxID)
                .Index(t => t.TaxID)
                .Index(t => t.CategoryID)
                .Index(t => t.ProducerID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        ParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Categories", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        ProducerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProducerID);
            
            CreateTable(
                "dbo.Product_File",
                c => new
                    {
                        Product_FileID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_FileID)
                .ForeignKey("dbo.Files", t => t.FileID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.FileID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContentType = c.String(),
                        Path = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FileID);
            
            CreateTable(
                "dbo.Product_Tag",
                c => new
                    {
                        Product_TagID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_TagID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.Taxes",
                c => new
                    {
                        TaxID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TaxID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "TaxID", "dbo.Taxes");
            DropForeignKey("dbo.Product_Tag", "TagID", "dbo.Tags");
            DropForeignKey("dbo.Product_Tag", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Product_Order", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Product_File", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Product_File", "FileID", "dbo.Files");
            DropForeignKey("dbo.Products", "ProducerID", "dbo.Producers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentID", "dbo.Categories");
            DropForeignKey("dbo.Product_Order", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PaymentMethodID", "dbo.PaymentMethods");
            DropForeignKey("dbo.Invoices", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DeliveryMethodID", "dbo.DeliveryMethods");
            DropForeignKey("dbo.Orders", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Product_Tag", new[] { "TagID" });
            DropIndex("dbo.Product_Tag", new[] { "ProductID" });
            DropIndex("dbo.Product_File", new[] { "FileID" });
            DropIndex("dbo.Product_File", new[] { "ProductID" });
            DropIndex("dbo.Categories", new[] { "ParentID" });
            DropIndex("dbo.Products", new[] { "ProducerID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "TaxID" });
            DropIndex("dbo.Product_Order", new[] { "OrderID" });
            DropIndex("dbo.Product_Order", new[] { "ProductID" });
            DropIndex("dbo.Invoices", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "PaymentMethodID" });
            DropIndex("dbo.Orders", new[] { "DeliveryMethodID" });
            DropIndex("dbo.Orders", new[] { "AddressID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Addresses", new[] { "UserID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Taxes");
            DropTable("dbo.Tags");
            DropTable("dbo.Product_Tag");
            DropTable("dbo.Files");
            DropTable("dbo.Product_File");
            DropTable("dbo.Producers");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Product_Order");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Invoices");
            DropTable("dbo.DeliveryMethods");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
