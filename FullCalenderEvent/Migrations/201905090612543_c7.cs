namespace FullCalenderEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.eventDdays", "DdayEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.eventDdays", "DdayEnd");
        }
    }
}
