namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Miscs", "RID", "dbo.DatabaseObjects");
			DropIndex( "dbo.Miscs", new[] { "RID" } );
			RenameTable( name: "dbo.Miscs", newName: "Staffs" );
            AddColumn("dbo.Religions", "PrayerRequest", c => c.String());
            CreateIndex("dbo.Staffs", "RID");
            AddForeignKey("dbo.Staffs", "RID", "dbo.DatabaseObjects", "RID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "RID", "dbo.DatabaseObjects");
			DropIndex( "dbo.Staffs", new[] { "RID" } );
			RenameTable( name: "dbo.Staffs", newName: "Miscs" );
            DropColumn("dbo.Religions", "PrayerRequest");
            CreateIndex("dbo.Miscs", "RID");
            AddForeignKey("dbo.Miscs", "RID", "dbo.DatabaseObjects", "RID");
        }
    }
}
