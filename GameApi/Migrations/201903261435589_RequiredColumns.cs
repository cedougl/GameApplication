namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Achievements", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Statistics", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Matches", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Matches", "Name", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Statistics", "Name", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Achievements", "Name", c => c.String(maxLength: 255, unicode: false));
        }
    }
}
