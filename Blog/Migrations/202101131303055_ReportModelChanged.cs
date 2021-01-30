namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsInvestigated", c => c.Boolean());
            AddColumn("dbo.Reports", "ReportTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "ReportTime");
            DropColumn("dbo.Reports", "IsInvestigated");
        }
    }
}
