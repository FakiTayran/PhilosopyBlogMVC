namespace BlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeSystemWork1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Likes", "Islike", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Likes", "Islike", c => c.Boolean(nullable: false));
        }
    }
}
