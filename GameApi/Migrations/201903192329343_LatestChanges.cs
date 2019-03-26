namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Achievements", "Description", c => c.String(maxLength: 2048, unicode: false));
            AlterColumn("dbo.Games", "Description", c => c.String(maxLength: 2048, unicode: false));
            AlterColumn("dbo.Statistics", "Description", c => c.String(maxLength: 2048, unicode: false));
            AlterColumn("dbo.Players", "Password", c => c.String(nullable: false, maxLength: 32, unicode: false));
            AlterColumn("dbo.Matches", "Description", c => c.String(maxLength: 2048, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Matches", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Players", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Statistics", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Games", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Achievements", "Description", c => c.String(maxLength: 255, unicode: false));
        }
    }
}
