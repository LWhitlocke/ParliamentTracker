namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorDatabaseAlterations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BillStageDetails", "DateStageOccurred", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BillStageDetails", "DateStageOccurred", c => c.DateTime(nullable: false));
        }
    }
}
