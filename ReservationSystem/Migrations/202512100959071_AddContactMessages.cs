namespace ReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        SenderName = c.String(nullable: false),
                        SenderEmail = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        MessageBody = c.String(nullable: false, maxLength: 1000),
                        SentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactMessages");
        }
    }
}
