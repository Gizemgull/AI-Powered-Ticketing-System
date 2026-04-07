namespace ReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "AuthorName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "AuthorName", c => c.String());
            DropColumn("dbo.Comments", "Email");
        }
    }
}
