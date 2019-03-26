namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GameId = c.Long(nullable: false),
                        PlayerId = c.Long(nullable: false),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.Long(nullable: false),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Alias = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.Long(nullable: false),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GameId = c.Long(nullable: false),
                        PlayerId = c.Long(nullable: false),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Kills = c.Int(nullable: false),
                        Deaths = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.Long(nullable: false),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GameId = c.Long(nullable: false),
                        PlayerId = c.Long(nullable: false),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Rank = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.Long(nullable: false),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "GameId", "dbo.Games");
            DropForeignKey("dbo.Statistics", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Achievements", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Statistics", "GameId", "dbo.Games");
            DropForeignKey("dbo.Achievements", "GameId", "dbo.Games");
            DropIndex("dbo.Matches", new[] { "PlayerId" });
            DropIndex("dbo.Matches", new[] { "GameId" });
            DropIndex("dbo.Statistics", new[] { "PlayerId" });
            DropIndex("dbo.Statistics", new[] { "GameId" });
            DropIndex("dbo.Achievements", new[] { "PlayerId" });
            DropIndex("dbo.Achievements", new[] { "GameId" });
            DropTable("dbo.Matches");
            DropTable("dbo.Statistics");
            DropTable("dbo.Games");
            DropTable("dbo.Achievements");
        }
    }
}
