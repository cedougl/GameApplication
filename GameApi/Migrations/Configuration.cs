namespace GameApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GameApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GameApi.Models.GameDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(GameApi.Models.GameDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Players.AddOrUpdate(x => x.Id,
                new Player() { Id = 1, Username = "joec", Password = "Test123", FirstName = "Joe", LastName = "Cranston", Email = "joec@gmail.com", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Player() { Id = 2, Username = "johnw", Password = "Test123", FirstName = "John", LastName = "Wiley", Email = "johnw@gmail.com", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Player() { Id = 3, Username = "bobf", Password = "Test123", FirstName = "Bob", LastName = "Fortin", Email = "bobf@gmail.com", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Player() { Id = 4, Username = "maxl", Password = "Test123", FirstName = "Max", LastName = "Lockett", Email = "maxl@gmail.com", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Player() { Id = 5, Username = "larryi", Password = "Test123", FirstName = "Larry", LastName = "Izo", Email = "larryi@gmail.com", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Player() { Id = 6, Username = "maryf", Password = "Test123", FirstName = "Mary", LastName = "Franklin", Email = "mfranklin@gmail.com", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now }
                );

            context.Games.AddOrUpdate(x => x.Id,
                new Game() { Id = 1, Name = "Mortal Kombat 11", Alias = "MK11", Description = "Mortal Kombat 11 is a fighting game. Alongside the returning Fatalities and Brutalities, new gameplay features are introduced, such as Fatal Blow and Krushing Blow.", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Game() { Id = 2, Name = "Hitman 2", Alias = "HM2", Description = "Hitman 2: Silent Assassin is a stealth video game.  In the game, players assume the role of a hired assassin known as Agent 47.", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Game() { Id = 3, Name = "Lego The Incredibles", Alias = "LTI", Description = "Lego The Incredibles is a Lego-themed action-adventure video game.   The game allows the player to control various super-heroes and villains alike from both films.", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now }
                );

            context.Statistics.AddOrUpdate(x => x.Id,
                new Statistic() { Id = 1, PlayerId = 1, GameId = 1, Name = "Joe's Mortal Kombat Stats", Description = "", Kills=15, Deaths=7, Wins=3, Losses=5, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Statistic() { Id = 2, PlayerId = 2, GameId = 2, Name = "John's Hitman 2 Stats", Description = "", Kills = 76, Deaths = 21, Wins = 85, Losses = 43, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Statistic() { Id = 3, PlayerId = 3, GameId = 3, Name = "Bob's Lego The Incredibles Stats", Description = "", Kills = 157, Deaths = 78, Wins = 21, Losses = 18, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Statistic() { Id = 4, PlayerId = 5, GameId = 1, Name = "Larry's Mortal Kombat Stats", Description = "", Kills = 10, Deaths = 17, Wins = 13, Losses = 12, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now }
                );

            context.Achievements.AddOrUpdate(x => x.Id,
                new Achievement() { Id = 1, PlayerId = 1, GameId = 1, Name = "Bonus Lives", Description = "Bonus for reaching next level", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Achievement() { Id = 2, PlayerId = 2, GameId = 2, Name = "Perfect Kill", Description = "Amazing performance award", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Achievement() { Id = 3, PlayerId = 3, GameId = 3, Name = "Extra Lego Coins", Description = "Bonus coins for your recent accomplishment", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Achievement() { Id = 4, PlayerId = 4, GameId = 2, Name = "Holiday Bonus", Description = "Extra Holiday Bonus", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Achievement() { Id = 5, PlayerId = 5, GameId = 2, Name = "Speed Bonus", Description = "Fast Tke Down Bonus", CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now }
            );

            context.Matches.AddOrUpdate(x => x.Id,
                new Match() { Id = 1, PlayerId = 1, GameId = 2, Name = "Full On Battle", Description = "", Rank = 2, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 1, PlayerId = 2, GameId = 2, Name = "Full On Battle", Description = "", Rank = 3, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 1, PlayerId = 3, GameId = 2, Name = "Full On Battle", Description = "", Rank = 1, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 2, PlayerId = 1, GameId = 1, Name = "Seek and Destroy", Description = "", Rank = 1, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 2, PlayerId = 2, GameId = 1, Name = "Seek and Destroy", Description = "", Rank = 2, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 2, PlayerId = 3, GameId = 1, Name = "Seek and Destroy", Description = "", Rank = 3, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 3, PlayerId = 4, GameId = 1, Name = "Take Down", Description = "Take down your enemies as fast as possible", Rank = 3, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 3, PlayerId = 5, GameId = 1, Name = "Take Down", Description = "Take down your enemies as fast as possible", Rank = 2, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now },
                new Match() { Id = 3, PlayerId = 6, GameId = 1, Name = "Take Down", Description = "Take down your enemies as fast as possible", Rank = 1, CreatedBy = 1, CreateTime = DateTime.Now, UpdatedBy = 1, UpdateTime = DateTime.Now }
            );
        }
    }
}
