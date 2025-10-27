using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // define dbsets for both games and reveiws
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // carefully establish the one-to-many relationship between game and review entities
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Game) // a review has one game
                .WithMany(g => g.Reviews) // a game can have many reviews
                .HasForeignKey(r => r.GameId) // foreign key for the review table
                .OnDelete(DeleteBehavior.Cascade); // delete reviews if a game is deleted

            modelBuilder.Entity<Review>().HasIndex(r => r.GameId);
        }
    }
}
