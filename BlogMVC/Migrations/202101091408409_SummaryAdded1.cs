namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SummaryAdded1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Summary", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Summary", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
