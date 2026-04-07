namespace ReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ImageUrl");
        }
    }
}
