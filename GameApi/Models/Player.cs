using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameApi.Models
{
    public class Player
    {
        // Id column - primary key
        [Key]
        public long Id { get; set; }

        // User Name column
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Username { get; set; }

        // MD5 generates a 128-bit has value.  Therefore, we can store it in a 32 character string.
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(32)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

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