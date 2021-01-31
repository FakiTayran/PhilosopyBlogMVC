namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportSystemFixed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reports", name: "ApplıcationUserId", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Reports", name: "IX_ApplıcationUserId", newName: "IX_ApplicationUserId");
            CreateTable(
                "dbo.ReportCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reports", "ReportCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reports", "ReportCategoryId");
            AddForeignKey("dbo.Reports", "ReportCategoryId", "dbo.ReportCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "ReportCategoryId", "dbo.ReportCategories");
            DropIndex("dbo.Reports", new[] { "ReportCategoryId" });
            DropColumn("dbo.Reports", "ReportCategoryId");
            DropTable("dbo.ReportCategories");
            RenameIndex(table: "dbo.Reports", name: "IX_ApplicationUserId", newName: "IX_ApplıcationUserId");
            RenameColumn(table: "dbo.Reports", name: "ApplicationUserId", newName: "ApplıcationUserId");
        }
    }
}
