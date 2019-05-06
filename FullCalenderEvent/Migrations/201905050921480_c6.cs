namespace FullCalenderEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.eventDdays", "mainColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.eventDdays", "mainColor");
        }
    }
}
