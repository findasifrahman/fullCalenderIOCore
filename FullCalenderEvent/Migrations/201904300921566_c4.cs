namespace FullCalenderEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.eventDdays", "isFullDayMain", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.eventDdays", "isFullDayMain");
        }
    }
}
