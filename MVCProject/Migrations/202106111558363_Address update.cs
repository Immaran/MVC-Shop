namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addressupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "ApartmentNumber", c => c.String());
            DropColumn("dbo.Addresses", "ApartmentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "ApartmentNumber", c => c.String());
            DropColumn("dbo.Addresses", "ApartmentNumber");
        }
    }
}
