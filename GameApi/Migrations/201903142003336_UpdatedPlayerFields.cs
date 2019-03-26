namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPlayerFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "Username", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "Password", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "Email", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "FirstName", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Players", "LastName", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "LastName", c => c.String(unicode: false));
            AlterColumn("dbo.Players", "FirstName", c => c.String(unicode: false));
            AlterColumn("dbo.Players", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.Players", "Password", c => c.String(unicode: false));
            AlterColumn("dbo.Players", "Username", c => c.String(unicode: false));
        }
    }
}
