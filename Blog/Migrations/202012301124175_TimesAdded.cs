namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ArticleTime", c => c.DateTime());
            AddColumn("dbo.Comments", "CommentTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CommentTime");
            DropColumn("dbo.Articles", "ArticleTime");
        }
    }
}
