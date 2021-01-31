namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportSystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        ApplıcationUserId = c.String(maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplıcationUserId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.ApplıcationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Reports", "ApplıcationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reports", new[] { "ApplıcationUserId" });
            DropIndex("dbo.Reports", new[] { "ArticleId" });
            DropTable("dbo.Reports");
        }
    }
}
