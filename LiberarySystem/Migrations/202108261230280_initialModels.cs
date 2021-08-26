namespace LiberarySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.Int(nullable: false),
                        Author = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        AvalibleQuantity = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BorrowInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.Int(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReturnInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnInvoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ReturnInvoices", "BookId", "dbo.Books");
            DropForeignKey("dbo.BorrowInvoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BorrowInvoices", "BookId", "dbo.Books");
            DropIndex("dbo.ReturnInvoices", new[] { "CustomerId" });
            DropIndex("dbo.ReturnInvoices", new[] { "BookId" });
            DropIndex("dbo.BorrowInvoices", new[] { "CustomerId" });
            DropIndex("dbo.BorrowInvoices", new[] { "BookId" });
            DropTable("dbo.ReturnInvoices");
            DropTable("dbo.Customers");
            DropTable("dbo.BorrowInvoices");
            DropTable("dbo.Books");
        }
    }
}
