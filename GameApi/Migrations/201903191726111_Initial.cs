namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.Long(nullable: false),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Players");
        }
    }
}
