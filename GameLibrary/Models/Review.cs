using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibrary.Models
{
    public class Review
    {
        // an id for the review and another id which matches the corresponding gam
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; } 

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }

        [StringLength(60)]
        public string? ReviewerName { get; set; }

        // Navigation
        public Game? Game { get; set; }
    }
}
