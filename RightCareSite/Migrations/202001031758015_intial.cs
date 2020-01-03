namespace RightCareSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buy_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        Suply_TblId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suply_tbl", t => t.Suply_TblId, cascadeDelete: true)
                .Index(t => t.Suply_TblId);
            
            CreateTable(
                "dbo.Suply_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        GovernorateId = c.Int(nullable: false),
                        Stor_tbl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stor_tbl", t => t.Stor_tbl_Id)
                .ForeignKey("dbo.Governorates", t => t.GovernorateId, cascadeDelete: true)
                .Index(t => t.GovernorateId)
                .Index(t => t.Stor_tbl_Id);
            
            CreateTable(
                "dbo.Governorates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gov_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CUST_TBL",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CUST_NAME = c.String(),
                        CUST_TEL = c.String(),
                        CUST_ADD = c.String(),
                        MND_TBLId = c.Int(nullable: false),
                        GovernorateId = c.Int(nullable: false),
                        Stor_tbl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Governorates", t => t.GovernorateId, cascadeDelete: true)
                .ForeignKey("dbo.MND_TBL", t => t.MND_TBLId, cascadeDelete: true)
                .ForeignKey("dbo.Stor_tbl", t => t.Stor_tbl_Id)
                .Index(t => t.MND_TBLId)
                .Index(t => t.GovernorateId)
                .Index(t => t.Stor_tbl_Id);
            
            CreateTable(
                "dbo.MND_TBL",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MND_NAME = c.String(),
                        MND_ID = c.Double(nullable: false),
                        MND_TEL = c.String(),
                        MND_ADD = c.String(),
                        MND_REGON = c.String(),
                        START_DATE = c.DateTime(),
                        MND_CATId = c.Int(nullable: false),
                        Gov_tblId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gov_tbl", t => t.Gov_tblId, cascadeDelete: true)
                .ForeignKey("dbo.MND_CAT", t => t.MND_CATId, cascadeDelete: true)
                .Index(t => t.MND_CATId)
                .Index(t => t.Gov_tblId);
            
            CreateTable(
                "dbo.Gov_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gov_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MND_CAT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stor_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_TbleId = c.Int(nullable: false),
                        Input = c.Int(nullable: false),
                        Output = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        desc = c.Single(nullable: false),
                        OpNo = c.Int(nullable: false),
                        OpKind = c.String(),
                        Date = c.DateTime(),
                        sal = c.Int(nullable: false),
                        Rsal = c.Int(nullable: false),
                        Buy = c.Int(nullable: false),
                        Rbuy = c.Int(nullable: false),
                        MND_TBL_Id = c.Int(),
                        Rsal_tbl_Id = c.Int(),
                        Rbuy_tbl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MND_TBL", t => t.MND_TBL_Id)
                .ForeignKey("dbo.Product_Tble", t => t.Product_TbleId, cascadeDelete: true)
                .ForeignKey("dbo.Rsal_tbl", t => t.Rsal_tbl_Id)
                .ForeignKey("dbo.Rbuy_tbl", t => t.Rbuy_tbl_Id)
                .Index(t => t.Product_TbleId)
                .Index(t => t.MND_TBL_Id)
                .Index(t => t.Rsal_tbl_Id)
                .Index(t => t.Rbuy_tbl_Id);
            
            CreateTable(
                "dbo.Product_Tble",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SelPrice = c.Single(nullable: false),
                        Category_tblId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category_tbl", t => t.Category_tblId, cascadeDelete: true)
                .Index(t => t.Category_tblId);
            
            CreateTable(
                "dbo.Category_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rsal_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cust_TBLId = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CUST_TBL", t => t.Cust_TBLId, cascadeDelete: true)
                .Index(t => t.Cust_TBLId);
            
            CreateTable(
                "dbo.Sal_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cust_TBLId = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CUST_TBL", t => t.Cust_TBLId, cascadeDelete: true)
                .Index(t => t.Cust_TBLId);
            
            CreateTable(
                "dbo.Rbuy_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        Suply_TblId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suply_tbl", t => t.Suply_TblId, cascadeDelete: true)
                .Index(t => t.Suply_TblId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rbuy_tbl", "Suply_TblId", "dbo.Suply_tbl");
            DropForeignKey("dbo.Stor_tbl", "Rbuy_tbl_Id", "dbo.Rbuy_tbl");
            DropForeignKey("dbo.Suply_tbl", "GovernorateId", "dbo.Governorates");
            DropForeignKey("dbo.Sal_tbl", "Cust_TBLId", "dbo.CUST_TBL");
            DropForeignKey("dbo.Stor_tbl", "Rsal_tbl_Id", "dbo.Rsal_tbl");
            DropForeignKey("dbo.Rsal_tbl", "Cust_TBLId", "dbo.CUST_TBL");
            DropForeignKey("dbo.Suply_tbl", "Stor_tbl_Id", "dbo.Stor_tbl");
            DropForeignKey("dbo.Stor_tbl", "Product_TbleId", "dbo.Product_Tble");
            DropForeignKey("dbo.Product_Tble", "Category_tblId", "dbo.Category_tbl");
            DropForeignKey("dbo.Stor_tbl", "MND_TBL_Id", "dbo.MND_TBL");
            DropForeignKey("dbo.CUST_TBL", "Stor_tbl_Id", "dbo.Stor_tbl");
            DropForeignKey("dbo.MND_TBL", "MND_CATId", "dbo.MND_CAT");
            DropForeignKey("dbo.MND_TBL", "Gov_tblId", "dbo.Gov_tbl");
            DropForeignKey("dbo.CUST_TBL", "MND_TBLId", "dbo.MND_TBL");
            DropForeignKey("dbo.CUST_TBL", "GovernorateId", "dbo.Governorates");
            DropForeignKey("dbo.Buy_tbl", "Suply_TblId", "dbo.Suply_tbl");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rbuy_tbl", new[] { "Suply_TblId" });
            DropIndex("dbo.Sal_tbl", new[] { "Cust_TBLId" });
            DropIndex("dbo.Rsal_tbl", new[] { "Cust_TBLId" });
            DropIndex("dbo.Product_Tble", new[] { "Category_tblId" });
            DropIndex("dbo.Stor_tbl", new[] { "Rbuy_tbl_Id" });
            DropIndex("dbo.Stor_tbl", new[] { "Rsal_tbl_Id" });
            DropIndex("dbo.Stor_tbl", new[] { "MND_TBL_Id" });
            DropIndex("dbo.Stor_tbl", new[] { "Product_TbleId" });
            DropIndex("dbo.MND_TBL", new[] { "Gov_tblId" });
            DropIndex("dbo.MND_TBL", new[] { "MND_CATId" });
            DropIndex("dbo.CUST_TBL", new[] { "Stor_tbl_Id" });
            DropIndex("dbo.CUST_TBL", new[] { "GovernorateId" });
            DropIndex("dbo.CUST_TBL", new[] { "MND_TBLId" });
            DropIndex("dbo.Suply_tbl", new[] { "Stor_tbl_Id" });
            DropIndex("dbo.Suply_tbl", new[] { "GovernorateId" });
            DropIndex("dbo.Buy_tbl", new[] { "Suply_TblId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rbuy_tbl");
            DropTable("dbo.Sal_tbl");
            DropTable("dbo.Rsal_tbl");
            DropTable("dbo.Category_tbl");
            DropTable("dbo.Product_Tble");
            DropTable("dbo.Stor_tbl");
            DropTable("dbo.MND_CAT");
            DropTable("dbo.Gov_tbl");
            DropTable("dbo.MND_TBL");
            DropTable("dbo.CUST_TBL");
            DropTable("dbo.Governorates");
            DropTable("dbo.Suply_tbl");
            DropTable("dbo.Buy_tbl");
        }
    }
}
