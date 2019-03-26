using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameApplication.Models
{
    public class Achievement
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public Game Game { get; set; }
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}