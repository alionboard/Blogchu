namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        ReportCategoryId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.ReportCategories", t => t.ReportCategoryId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.ReportCategoryId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "ReportCategoryId", "dbo.ReportCategories");
            DropForeignKey("dbo.Reports", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Reports", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reports", new[] { "ApplicationUserId" });
            DropIndex("dbo.Reports", new[] { "ReportCategoryId" });
            DropIndex("dbo.Reports", new[] { "ArticleId" });
            DropTable("dbo.Reports");
            DropTable("dbo.ReportCategories");
        }
    }
}
