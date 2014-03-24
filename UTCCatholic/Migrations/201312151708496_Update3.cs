namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "InterestedTalk", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "InterestedTalk");
        }
    }
}
