namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zero : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatabaseObjects",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Retreater = c.Boolean(),
                        RetreaterPending = c.Boolean(),
                        Staffer = c.Boolean(),
                        StafferPending = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Emergency_RID = c.Guid(),
                        Medical_RID = c.Guid(),
                        Personal_RID = c.Guid(),
                        Religion_RID = c.Guid(),
                        School_RID = c.Guid(),
                        Staff_RID = c.Guid(),
                        Transportation_RID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Emergencys", t => t.Emergency_RID)
                .ForeignKey("dbo.Medicals", t => t.Medical_RID)
                .ForeignKey("dbo.Personals", t => t.Personal_RID)
                .ForeignKey("dbo.Religions", t => t.Religion_RID)
                .ForeignKey("dbo.Schools", t => t.School_RID)
                .ForeignKey("dbo.Miscs", t => t.Staff_RID)
                .ForeignKey("dbo.Transportations", t => t.Transportation_RID)
                .Index(t => t.Emergency_RID)
                .Index(t => t.Medical_RID)
                .Index(t => t.Personal_RID)
                .Index(t => t.Religion_RID)
                .Index(t => t.School_RID)
                .Index(t => t.Staff_RID)
                .Index(t => t.Transportation_RID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        UserAgent = c.String(),
                        Connected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.RID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.StaffChoices",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.TShirtSizes",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Size = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Miscs",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        FirstChoice_RID = c.Guid(),
                        SecondChoice_RID = c.Guid(),
                        ThirdChoice_RID = c.Guid(),
                        PreviousAttended = c.String(),
                        PreviousPositions = c.String(),
                        PreviousTalk = c.String(),
                        LeadershipPosition = c.String(),
                        AdditionalComments = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.StaffChoices", t => t.FirstChoice_RID)
                .ForeignKey("dbo.StaffChoices", t => t.SecondChoice_RID)
                .ForeignKey("dbo.StaffChoices", t => t.ThirdChoice_RID)
                .Index(t => t.RID)
                .Index(t => t.FirstChoice_RID)
                .Index(t => t.SecondChoice_RID)
                .Index(t => t.ThirdChoice_RID);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Carrier_RID = c.Guid(nullable: false),
                        Number = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.Carriers", t => t.Carrier_RID)
                .Index(t => t.RID)
                .Index(t => t.Carrier_RID);
            
            CreateTable(
                "dbo.Religions",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        FavoriteSaint = c.String(),
                        FavoriteBibleVerse = c.String(),
                        RoleModel = c.String(),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Saints",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Grade_RID = c.Guid(),
                        College = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.Grades", t => t.Grade_RID)
                .Index(t => t.RID)
                .Index(t => t.Grade_RID);
            
            CreateTable(
                "dbo.Personals",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Phone_RID = c.Guid(),
                        TShirtSize_RID = c.Guid(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Hobbies = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.Phones", t => t.Phone_RID)
                .ForeignKey("dbo.TShirtSizes", t => t.TShirtSize_RID)
                .Index(t => t.RID)
                .Index(t => t.Phone_RID)
                .Index(t => t.TShirtSize_RID);
            
            CreateTable(
                "dbo.Emergencys",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Phone_RID = c.Guid(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .ForeignKey("dbo.Phones", t => t.Phone_RID)
                .Index(t => t.RID)
                .Index(t => t.Phone_RID);
            
            CreateTable(
                "dbo.Transportations",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        TransportSelf = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Medicals",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Allergies = c.String(),
                        HealthConditions = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.DatabaseObjects", t => t.RID)
                .Index(t => t.RID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicals", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Transportations", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Emergencys", "Phone_RID", "dbo.Phones");
            DropForeignKey("dbo.Emergencys", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Personals", "TShirtSize_RID", "dbo.TShirtSizes");
            DropForeignKey("dbo.Personals", "Phone_RID", "dbo.Phones");
            DropForeignKey("dbo.Personals", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Schools", "Grade_RID", "dbo.Grades");
            DropForeignKey("dbo.Schools", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Saints", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Religions", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Phones", "Carrier_RID", "dbo.Carriers");
            DropForeignKey("dbo.Phones", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Miscs", "ThirdChoice_RID", "dbo.StaffChoices");
            DropForeignKey("dbo.Miscs", "SecondChoice_RID", "dbo.StaffChoices");
            DropForeignKey("dbo.Miscs", "FirstChoice_RID", "dbo.StaffChoices");
            DropForeignKey("dbo.Miscs", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.TShirtSizes", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.StaffChoices", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Grades", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Connections", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Connections", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Carriers", "RID", "dbo.DatabaseObjects");
            DropForeignKey("dbo.Users", "Transportation_RID", "dbo.Transportations");
            DropForeignKey("dbo.Users", "Staff_RID", "dbo.Miscs");
            DropForeignKey("dbo.Users", "School_RID", "dbo.Schools");
            DropForeignKey("dbo.Users", "Religion_RID", "dbo.Religions");
            DropForeignKey("dbo.Users", "Personal_RID", "dbo.Personals");
            DropForeignKey("dbo.Users", "Medical_RID", "dbo.Medicals");
            DropForeignKey("dbo.Users", "Emergency_RID", "dbo.Emergencys");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropIndex("dbo.Medicals", new[] { "RID" });
            DropIndex("dbo.Transportations", new[] { "RID" });
            DropIndex("dbo.Emergencys", new[] { "Phone_RID" });
            DropIndex("dbo.Emergencys", new[] { "RID" });
            DropIndex("dbo.Personals", new[] { "TShirtSize_RID" });
            DropIndex("dbo.Personals", new[] { "Phone_RID" });
            DropIndex("dbo.Personals", new[] { "RID" });
            DropIndex("dbo.Schools", new[] { "Grade_RID" });
            DropIndex("dbo.Schools", new[] { "RID" });
            DropIndex("dbo.Saints", new[] { "RID" });
            DropIndex("dbo.Religions", new[] { "RID" });
            DropIndex("dbo.Phones", new[] { "Carrier_RID" });
            DropIndex("dbo.Phones", new[] { "RID" });
            DropIndex("dbo.Miscs", new[] { "ThirdChoice_RID" });
            DropIndex("dbo.Miscs", new[] { "SecondChoice_RID" });
            DropIndex("dbo.Miscs", new[] { "FirstChoice_RID" });
            DropIndex("dbo.Miscs", new[] { "RID" });
            DropIndex("dbo.TShirtSizes", new[] { "RID" });
            DropIndex("dbo.StaffChoices", new[] { "RID" });
            DropIndex("dbo.Grades", new[] { "RID" });
            DropIndex("dbo.Connections", new[] { "User_Id" });
            DropIndex("dbo.Connections", new[] { "RID" });
            DropIndex("dbo.Carriers", new[] { "RID" });
            DropIndex("dbo.Users", new[] { "Transportation_RID" });
            DropIndex("dbo.Users", new[] { "Staff_RID" });
            DropIndex("dbo.Users", new[] { "School_RID" });
            DropIndex("dbo.Users", new[] { "Religion_RID" });
            DropIndex("dbo.Users", new[] { "Personal_RID" });
            DropIndex("dbo.Users", new[] { "Medical_RID" });
            DropIndex("dbo.Users", new[] { "Emergency_RID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.Medicals");
            DropTable("dbo.Transportations");
            DropTable("dbo.Emergencys");
            DropTable("dbo.Personals");
            DropTable("dbo.Schools");
            DropTable("dbo.Saints");
            DropTable("dbo.Religions");
            DropTable("dbo.Phones");
            DropTable("dbo.Miscs");
            DropTable("dbo.TShirtSizes");
            DropTable("dbo.StaffChoices");
            DropTable("dbo.Grades");
            DropTable("dbo.Connections");
            DropTable("dbo.Carriers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.DatabaseObjects");
        }
    }
}
