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
                        DateTime = c.DateTime(nullable: false),
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
                        Suply_TblId = c.Int(nullable: false),
                        CUST_TBLId = c.Int(nullable: false),
                        MND_TBL_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CUST_TBL", t => t.CUST_TBLId, cascadeDelete: true)
                .ForeignKey("dbo.MND_TBL", t => t.MND_TBL_Id)
                .ForeignKey("dbo.Product_Tble", t => t.Product_TbleId, cascadeDelete: true)
                .ForeignKey("dbo.Suply_tbl", t => t.Suply_TblId, cascadeDelete: true)
                .Index(t => t.Product_TbleId)
                .Index(t => t.Suply_TblId)
                .Index(t => t.CUST_TBLId)
                .Index(t => t.MND_TBL_Id);
            
            CreateTable(
                "dbo.MND_CAT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sal_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cust_TBLId = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CUST_TBL", t => t.Cust_TBLId, cascadeDelete: true)
                .Index(t => t.Cust_TBLId);
            
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
            
            AddColumn("dbo.MND_TBL", "MND_CATId", c => c.Int(nullable: false));
            CreateIndex("dbo.MND_TBL", "MND_CATId");
            AddForeignKey("dbo.MND_TBL", "MND_CATId", "dbo.MND_CAT", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stor_tbl", "Suply_TblId", "dbo.Suply_tbl");
            DropForeignKey("dbo.Stor_tbl", "Product_TbleId", "dbo.Product_Tble");
            DropForeignKey("dbo.Product_Tble", "Category_tblId", "dbo.Category_tbl");
            DropForeignKey("dbo.Stor_tbl", "MND_TBL_Id", "dbo.MND_TBL");
            DropForeignKey("dbo.Stor_tbl", "CUST_TBLId", "dbo.CUST_TBL");
            DropForeignKey("dbo.Sal_tbl", "Cust_TBLId", "dbo.CUST_TBL");
            DropForeignKey("dbo.MND_TBL", "MND_CATId", "dbo.MND_CAT");
            DropForeignKey("dbo.Buy_tbl", "Suply_TblId", "dbo.Suply_tbl");
            DropIndex("dbo.Product_Tble", new[] { "Category_tblId" });
            DropIndex("dbo.Sal_tbl", new[] { "Cust_TBLId" });
            DropIndex("dbo.MND_TBL", new[] { "MND_CATId" });
            DropIndex("dbo.Stor_tbl", new[] { "MND_TBL_Id" });
            DropIndex("dbo.Stor_tbl", new[] { "CUST_TBLId" });
            DropIndex("dbo.Stor_tbl", new[] { "Suply_TblId" });
            DropIndex("dbo.Stor_tbl", new[] { "Product_TbleId" });
            DropIndex("dbo.Buy_tbl", new[] { "Suply_TblId" });
            DropColumn("dbo.MND_TBL", "MND_CATId");
            DropTable("dbo.Category_tbl");
            DropTable("dbo.Product_Tble");
            DropTable("dbo.Sal_tbl");
            DropTable("dbo.MND_CAT");
            DropTable("dbo.Stor_tbl");
            DropTable("dbo.Suply_tbl");
            DropTable("dbo.Buy_tbl");
        }
    }
}
