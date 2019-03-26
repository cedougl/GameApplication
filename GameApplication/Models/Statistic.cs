using System;

namespace GameApplication.Models
{
    /// <summary>
    /// This class is the Statistic model class for the view, which matches the model class in the Web API
    /// </summary>
    public class Statistic
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public Game Game { get; set; }
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}