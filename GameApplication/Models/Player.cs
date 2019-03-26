using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameApplication.Models
{
    public class Player
    {
        // Id column - primary key
        public long Id { get; set; }

        // User Name column
        public string Username { get; set; }

        // MD5 generates a 128-bit has value.  Therefore, we can store it in a 32 character string.
        public string Password { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public ICollection<Achievement> Achievements { get; set; }
        //public ICollection<Statistic> Statistics { get; set; }

        // Database table columns used for auditing
        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}