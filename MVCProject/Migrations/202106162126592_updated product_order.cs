namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedproduct_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product_Order", "ProductQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product_Order", "ProductQuantity");
        }
    }
}
