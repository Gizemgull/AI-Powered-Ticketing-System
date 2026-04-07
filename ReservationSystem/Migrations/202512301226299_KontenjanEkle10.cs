namespace ReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KontenjanEkle10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Quota", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Quota");
        }
    }
}
