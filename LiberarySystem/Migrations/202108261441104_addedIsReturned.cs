namespace LiberarySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsReturned : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BorrowInvoices", "IsReturned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BorrowInvoices", "IsReturned");
        }
    }
}
