namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        CurrentHouse = c.String(),
                        OriginatedHouse = c.String(),
                        LastUpdated = c.DateTime(nullable: false),
                        Uri = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillStageDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StageId = c.Long(nullable: false),
                        BillStageId = c.Int(nullable: false),
                        DateStageOccurred = c.DateTime(nullable: false),
                        LinkUri = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillStages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrawlBillDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CrawlDetailsId = c.Long(nullable: false),
                        BillId = c.Long(nullable: false),
                        Started = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrawlDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Started = c.DateTime(nullable: false),
                        Completed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CrawlDetails");
            DropTable("dbo.CrawlBillDetails");
            DropTable("dbo.BillStages");
            DropTable("dbo.BillStageDetails");
            DropTable("dbo.Bills");
        }
    }
}
