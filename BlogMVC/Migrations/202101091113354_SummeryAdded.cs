namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SummeryAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Summery", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Summery");
        }
    }
}
