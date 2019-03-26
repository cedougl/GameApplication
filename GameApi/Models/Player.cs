using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameApi.Models
{
    /// <summary>
    /// The Player class is the database model object corresponding to the Player database table
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Id column - primary key
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Username column
        /// </summary>
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Password column - MD5 generates a 128-bit has value.  Therefore, we can store it in a 32 character string
        /// </summary>
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(32)]
        public string Password { get; set; }

        /// <summary>
        /// Email column
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// FirstName column
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName column
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Collection of achievements for this player
        /// </summary>
        public ICollection<Achievement> Achievements { get; set; }

        /// <summary>
        /// Collection of statistics recorded for this player
        /// </summary>
        public ICollection<Statistic> Statistics { get; set; }

        // Database table columns used for auditing

        /// <summary>
        /// CreateTime column - The date and time of when this player record was created
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// CreatedBy column - User ID of the user who created this player record
        /// </summary>
        [Required]
        public long CreatedBy { get; set; }

        /// <summary>
        /// UpdateTime - The date and time of when this player record was last updated  
        /// </summary>
        [Required]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// UpdatedBy column - User ID of the user who last updated this player record
        /// </summary>
        [Required]
        public long UpdatedBy { get; set; }
    }
}
