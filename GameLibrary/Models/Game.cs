using System.ComponentModel.DataAnnotations;

namespace GameLibrary.Models
{
    public class Game
    {
        // establish all basic properties for each game
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = string.Empty;

        [StringLength(60)]
        public string? Platform { get; set; } 

        [StringLength(60)]
        public string? Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Range(0, 1000)]
        public decimal? Price { get; set; }
    
        // every game will have a collection of reviews
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
