namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            AddColumn("dbo.Orders", "StatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "StatusID");
            AddForeignKey("dbo.Orders", "StatusID", "dbo.Status", "StatusID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StatusID", "dbo.Status");
            DropIndex("dbo.Orders", new[] { "StatusID" });
            DropColumn("dbo.Orders", "StatusID");
            DropTable("dbo.Status");
        }
    }
}
