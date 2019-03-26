using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameApi.Models
{
    /// <summary>
    /// The Game class is the database model object corresponding to the Game database table
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Id column - primary key
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Name column - Name of the game
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Alias column - Optional alias of the game
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Description column - Optional description of the game
        /// </summary>
        [Column(TypeName = "varchar")]
        [StringLength(2048)]
        public string Description { get; set; }

        /// <summary>
        /// Collection of game achievements
        /// </summary>
        public ICollection<Achievement> Achievements { get; set; }

        /// <summary>
        /// Collection of game statistics
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