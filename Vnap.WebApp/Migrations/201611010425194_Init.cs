namespace Vnap.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Caption = c.String(),
                        ThumbnailUrl = c.String(),
                        Priority = c.Int(nullable: false),
                        PlantDiseaseId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUser_Id)
                .ForeignKey("dbo.PlantDiseases", t => t.PlantDiseaseId, cascadeDelete: true)
                .Index(t => t.PlantDiseaseId)
                .Index(t => t.CreatedUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PlantDiseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Avatar = c.String(),
                        Priority = c.Int(nullable: false),
                        PlantDiseaseType = c.Int(nullable: false),
                        PlantId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUser_Id)
                .ForeignKey("dbo.Plants", t => t.PlantId, cascadeDelete: true)
                .Index(t => t.PlantId)
                .Index(t => t.CreatedUser_Id);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Avatar = c.String(),
                        Priority = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUser_Id)
                .Index(t => t.CreatedUser_Id);
            
            CreateTable(
                "dbo.Solutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyName = c.String(),
                        Avatar = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        Prime = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser_Id = c.String(maxLength: 128),
                        PlantDisease_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUser_Id)
                .ForeignKey("dbo.PlantDiseases", t => t.PlantDisease_Id)
                .Index(t => t.CreatedUser_Id)
                .Index(t => t.PlantDisease_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Solutions", "PlantDisease_Id", "dbo.PlantDiseases");
            DropForeignKey("dbo.Solutions", "CreatedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PlantDiseases", "PlantId", "dbo.Plants");
            DropForeignKey("dbo.Plants", "CreatedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Images", "PlantDiseaseId", "dbo.PlantDiseases");
            DropForeignKey("dbo.PlantDiseases", "CreatedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Images", "CreatedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Solutions", new[] { "PlantDisease_Id" });
            DropIndex("dbo.Solutions", new[] { "CreatedUser_Id" });
            DropIndex("dbo.Plants", new[] { "CreatedUser_Id" });
            DropIndex("dbo.PlantDiseases", new[] { "CreatedUser_Id" });
            DropIndex("dbo.PlantDiseases", new[] { "PlantId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Images", new[] { "CreatedUser_Id" });
            DropIndex("dbo.Images", new[] { "PlantDiseaseId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Solutions");
            DropTable("dbo.Plants");
            DropTable("dbo.PlantDiseases");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Images");
        }
    }
}
