using DAL.Models;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ParliamentBillsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParliamentBillsContext context)
        {
            context.BillStages.AddOrUpdate(x => x.Id, 
                new BillStage(){ Id = 1, Name = "Commons First reading"},
                new BillStage() { Id = 2, Name = "Commons Second reading" },
                new BillStage() { Id = 3, Name = "Commons Committee stage" },
                new BillStage() { Id = 4, Name = "Commons Report stage" },
                new BillStage() { Id = 5, Name = "Commons Third reading" },
                new BillStage() { Id = 6, Name = "Lords First reading" },
                new BillStage() { Id = 7, Name = "Lords Second reading" },
                new BillStage() { Id = 8, Name = "Lords Committee stage" },
                new BillStage() { Id = 9, Name = "Lords Report stage" },
                new BillStage() { Id = 10, Name = "Lords Third reading" },
                new BillStage() { Id = 11, Name = "Consideration of amendments" },
                new BillStage() { Id = 12, Name = "Pending Royal Assent" });

            context.SaveChanges();
        }
    }
}
