using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameApplication.Models
{
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public string Description { get; set; }

        //public ICollection<Achievement> Achievements { get; set; }
        //public ICollection<Statistic> Statistics { get; set; }

        // Database table columns used for auditing
        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}