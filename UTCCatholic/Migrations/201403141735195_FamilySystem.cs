namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FamilySystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Gopher_Id = c.String(maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.Users", t => t.Gopher_Id)
                .Index(t => t.RID)
                .Index(t => t.Gopher_Id);
            
            AddColumn("dbo.Users", "ApplicationStamp", c => c.DateTime());
            AddColumn("dbo.Users", "Family_RID", c => c.Guid());
            AddColumn("dbo.Users", "Family_RID1", c => c.Guid());
            AddColumn("dbo.Users", "Family_RID2", c => c.Guid());
            AddColumn("dbo.Users", "Family_RID3", c => c.Guid());
            CreateIndex("dbo.Users", "Family_RID");
            CreateIndex("dbo.Users", "Family_RID1");
            CreateIndex("dbo.Users", "Family_RID2");
            CreateIndex("dbo.Users", "Family_RID3");
            AddForeignKey("dbo.Users", "Family_RID", "dbo.Families", "RID");
            AddForeignKey("dbo.Users", "Family_RID1", "dbo.Families", "RID");
            AddForeignKey("dbo.Users", "Family_RID2", "dbo.Families", "RID");
            AddForeignKey("dbo.Users", "Family_RID3", "dbo.Families", "RID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Families", "Gopher_Id", "dbo.Users");
            DropForeignKey("dbo.Families", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Users", "Family_RID3", "dbo.Families");
            DropForeignKey("dbo.Users", "Family_RID2", "dbo.Families");
            DropForeignKey("dbo.Users", "Family_RID1", "dbo.Families");
            DropForeignKey("dbo.Users", "Family_RID", "dbo.Families");
            DropIndex("dbo.Families", new[] { "Gopher_Id" });
            DropIndex("dbo.Families", new[] { "RID" });
            DropIndex("dbo.Users", new[] { "Family_RID3" });
            DropIndex("dbo.Users", new[] { "Family_RID2" });
            DropIndex("dbo.Users", new[] { "Family_RID1" });
            DropIndex("dbo.Users", new[] { "Family_RID" });
            DropColumn("dbo.Users", "Family_RID3");
            DropColumn("dbo.Users", "Family_RID2");
            DropColumn("dbo.Users", "Family_RID1");
            DropColumn("dbo.Users", "Family_RID");
            DropColumn("dbo.Users", "ApplicationStamp");
            DropTable("dbo.Families");
        }
    }
}
