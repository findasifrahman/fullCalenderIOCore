namespace FullCalenderEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.eventDdays", "ReFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.eventDdays", "ReFile");
        }
    }
}
