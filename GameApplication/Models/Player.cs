using System;

namespace GameApplication.Models
{
    /// <summary>
    /// This class is the Player model class for the view, which matches the model class in the Web API
    /// </summary>
    public class Player
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Database table columns used for auditing
        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}