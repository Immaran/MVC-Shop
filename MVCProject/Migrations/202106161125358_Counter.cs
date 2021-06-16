namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Counter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        CounterId = c.Int(nullable: false, identity: true),
                        CounterNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CounterId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Counters");
        }
    }
}
