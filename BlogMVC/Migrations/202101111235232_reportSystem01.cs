namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportSystem01 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reports", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "Content", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
