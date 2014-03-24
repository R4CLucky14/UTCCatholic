namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FamilySystem3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Family_RID1", "dbo.Families");
            DropIndex("dbo.Users", new[] { "Family_RID1" });
            DropColumn("dbo.Users", "Family_RID1");
            RenameColumn(table: "dbo.Users", name: "Family_RID2", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Users", name: "Family_RID3", newName: "Family_RID2");
            RenameColumn(table: "dbo.Users", name: "__mig_tmp__0", newName: "Family_RID1");
            AddColumn("dbo.Families", "Father_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Families", "Mother_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Families", "Father_Id");
            CreateIndex("dbo.Families", "Mother_Id");
            AddForeignKey("dbo.Families", "Father_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Families", "Mother_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Families", "Mother_Id", "dbo.Users");
            DropForeignKey("dbo.Families", "Father_Id", "dbo.Users");
            DropIndex("dbo.Families", new[] { "Mother_Id" });
            DropIndex("dbo.Families", new[] { "Father_Id" });
            DropColumn("dbo.Families", "Mother_Id");
            DropColumn("dbo.Families", "Father_Id");
            RenameColumn(table: "dbo.Users", name: "Family_RID1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Users", name: "Family_RID2", newName: "Family_RID3");
            RenameColumn(table: "dbo.Users", name: "__mig_tmp__0", newName: "Family_RID2");
            AddColumn("dbo.Users", "Family_RID1", c => c.Guid());
            CreateIndex("dbo.Users", "Family_RID1");
            AddForeignKey("dbo.Users", "Family_RID1", "dbo.Families", "RID");
        }
    }
}
