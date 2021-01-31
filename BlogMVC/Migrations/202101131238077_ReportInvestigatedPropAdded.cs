namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportInvestigatedPropAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsInvestigated", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "IsInvestigated");
        }
    }
}
