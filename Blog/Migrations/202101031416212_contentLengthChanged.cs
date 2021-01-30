namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contentLengthChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Content", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
