namespace FullCalenderEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.eventFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        EventName = c.String(),
                        fileFolder = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.eventFiles");
        }
    }
}
