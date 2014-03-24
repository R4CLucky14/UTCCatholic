namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Staffs", "InterestedTalk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staffs", "InterestedTalk", c => c.String());
        }
    }
}
