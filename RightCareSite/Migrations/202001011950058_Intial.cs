namespace RightCareSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gov_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gov_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MND_TBL", "Gov_tblId", c => c.Int(nullable: false));
            CreateIndex("dbo.MND_TBL", "Gov_tblId");
            AddForeignKey("dbo.MND_TBL", "Gov_tblId", "dbo.Gov_tbl", "Id", cascadeDelete: true);
            Sql("INSERT INTO[dbo].[Gov_tbl]([Gov_Name]) VALUES('الفيوم')");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MND_TBL", "Gov_tblId", "dbo.Gov_tbl");
            DropIndex("dbo.MND_TBL", new[] { "Gov_tblId" });
            DropColumn("dbo.MND_TBL", "Gov_tblId");
            DropTable("dbo.Gov_tbl");
        }
    }
}
