namespace ReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 500),
                        Sentiment = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        ReservationDate = c.DateTime(nullable: false),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "EventID", "dbo.Events");
            DropForeignKey("dbo.Comments", "EventID", "dbo.Events");
            DropIndex("dbo.Reservations", new[] { "EventID" });
            DropIndex("dbo.Comments", new[] { "EventID" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Events");
            DropTable("dbo.Comments");
            DropTable("dbo.Admins");
        }
    }
}
