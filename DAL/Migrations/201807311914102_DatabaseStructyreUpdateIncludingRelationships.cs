namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseStructyreUpdateIncludingRelationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillStageDetails", "BillId", c => c.Long(nullable: false));
            AddColumn("dbo.CrawlBillDetails", "ExceptionDetails", c => c.String());
            CreateIndex("dbo.BillStageDetails", "BillId");
            CreateIndex("dbo.BillStageDetails", "BillStageId");
            CreateIndex("dbo.CrawlBillDetails", "CrawlDetailsId");
            CreateIndex("dbo.CrawlBillDetails", "BillId");
            AddForeignKey("dbo.BillStageDetails", "BillId", "dbo.Bills", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BillStageDetails", "BillStageId", "dbo.BillStages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrawlBillDetails", "BillId", "dbo.Bills", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrawlBillDetails", "CrawlDetailsId", "dbo.CrawlDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.BillStageDetails", "StageId");
            DropColumn("dbo.BillStageDetails", "LinkUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BillStageDetails", "LinkUri", c => c.String());
            AddColumn("dbo.BillStageDetails", "StageId", c => c.Long(nullable: false));
            DropForeignKey("dbo.CrawlBillDetails", "CrawlDetailsId", "dbo.CrawlDetails");
            DropForeignKey("dbo.CrawlBillDetails", "BillId", "dbo.Bills");
            DropForeignKey("dbo.BillStageDetails", "BillStageId", "dbo.BillStages");
            DropForeignKey("dbo.BillStageDetails", "BillId", "dbo.Bills");
            DropIndex("dbo.CrawlBillDetails", new[] { "BillId" });
            DropIndex("dbo.CrawlBillDetails", new[] { "CrawlDetailsId" });
            DropIndex("dbo.BillStageDetails", new[] { "BillStageId" });
            DropIndex("dbo.BillStageDetails", new[] { "BillId" });
            DropColumn("dbo.CrawlBillDetails", "ExceptionDetails");
            DropColumn("dbo.BillStageDetails", "BillId");
        }
    }
}
