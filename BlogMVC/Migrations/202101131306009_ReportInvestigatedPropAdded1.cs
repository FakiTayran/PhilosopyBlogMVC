namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportInvestigatedPropAdded1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "ReportTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "ReportTime");
        }
    }
}
