namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categoryupdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Categories", new[] { "ParentID" });
            AlterColumn("dbo.Categories", "ParentID", c => c.Int());
            CreateIndex("dbo.Categories", "ParentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "ParentID" });
            AlterColumn("dbo.Categories", "ParentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "ParentID");
        }
    }
}
