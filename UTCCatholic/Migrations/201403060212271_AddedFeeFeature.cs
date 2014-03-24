namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFeeFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FeePaid", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Users", "Fee", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Fee");
            DropColumn("dbo.Users", "FeePaid");
        }
    }
}
