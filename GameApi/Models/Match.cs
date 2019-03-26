using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameApi.Models
{
    /// <summary>
    /// The Match class is the database model object corresponding to the Match database table
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Id column - primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// GameId indexed column - Foreign key into the Game table
        /// </summary>
        [Required]
        [Index]
        public long GameId { get; set; }

        /// <summary>
        /// Corresponding Game object with the GameId foreign key
        /// </summary>
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        /// <summary>
        /// PlayerId indexed column - Foreign key into the Player table
        /// </summary>
        [Required]
        [Index]
        public long PlayerId { get; set; }

        /// <summary>
        /// Corresponding Player object with the PlayerId foreign key
        /// </summary>
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        /// <summary>
        /// Name column
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description column - Limited to 2048 characters
        /// </summary>
        [Column(TypeName = "varchar")]
        [StringLength(2048)]
        public string Description { get; set; }

        /// <summary>
        /// Rank column - Rank of the player in the match
        /// </summary>
        public int Rank { get; set; }

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