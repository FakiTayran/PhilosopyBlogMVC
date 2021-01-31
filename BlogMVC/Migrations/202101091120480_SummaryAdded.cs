namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SummaryAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Summary", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Articles", "Summery");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Summery", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Articles", "Summary");
        }
    }
}
