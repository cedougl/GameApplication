using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameApi.Models
{
    public class Match
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Index]
        public long GameId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        [Required]
        [Index]
        public long PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(2048)]
        public string Description { get; set; }

        public int Rank { get; set; }

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