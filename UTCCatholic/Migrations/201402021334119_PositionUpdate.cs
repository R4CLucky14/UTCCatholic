namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Position");
        }
    }
}
