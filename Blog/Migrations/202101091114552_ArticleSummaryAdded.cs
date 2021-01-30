namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleSummaryAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Summary", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Summary");
        }
    }
}
