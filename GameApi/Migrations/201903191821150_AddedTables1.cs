namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Achievements", "Name", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Achievements", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Games", "Alias", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Games", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Statistics", "Name", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Statistics", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Players", "Username", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Players", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Players", "Email", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Players", "FirstName", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Players", "LastName", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Matches", "Name", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Matches", "Description", c => c.String(maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Matches", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Matches", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Players", "LastName", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "FirstName", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "Email", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "Password", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "Username", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Statistics", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Statistics", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Games", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Games", "Alias", c => c.String(unicode: false));
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Achievements", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Achievements", "Name", c => c.String(unicode: false));
        }
    }
}
