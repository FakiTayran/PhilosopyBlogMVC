namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeSystemWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Likes", "Islike", c => c.Boolean(nullable: false));
            DropColumn("dbo.Likes", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Likes", "ApplicationUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Likes", "Islike");
        }
    }
}
