using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameApi.Models
{
    /// <summary>
    /// Database context object for the Game Management database
    /// </summary>
    public class GameDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GameDbContext() : base("name=GameDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(s => s.HasMaxLength(255).HasColumnType("varchar"));

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<GameApi.Models.Player> Players { get; set; }
        public System.Data.Entity.DbSet<GameApi.Models.Game> Games { get; set; }
        public System.Data.Entity.DbSet<GameApi.Models.Achievement> Achievements { get; set; }
        public System.Data.Entity.DbSet<GameApi.Models.Statistic> Statistics { get; set; }
        public System.Data.Entity.DbSet<GameApi.Models.Match> Matches { get; set; }
    }
}
