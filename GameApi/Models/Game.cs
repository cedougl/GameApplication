using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameApi.Models
{
    public class Game
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Alias { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(2048)]
        public string Description { get; set; }

        public ICollection<Achievement> Achievements { get; set; }
        public ICollection<Statistic> Statistics { get; set; }

        // Database table columns used for auditing
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        [Required]
        public DateTime UpdateTime { get; set; }
        [Required]
        public long UpdatedBy { get; set; }
    }
}