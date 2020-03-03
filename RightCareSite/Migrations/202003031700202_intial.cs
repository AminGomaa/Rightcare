namespace RightCareSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MainStocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MainStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prod_Id = c.Int(nullable: false),
                        Sub_Id = c.Int(nullable: false),
                        MndId = c.Int(nullable: false),
                        StQty = c.Single(nullable: false),
                        Case = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
