using System;

namespace GameApplication.Models
{
    /// <summary>
    /// This class is the Game model class for the view, which matches the model class in the Web API
    /// </summary>
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }

        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}