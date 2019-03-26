using System;

namespace GameApplication.Models
{
    /// <summary>
    /// This class is the Match model class for the view, which matches the model class in the Web API
    /// </summary>
    public class Match
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public Game Game { get; set; }
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }

        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}