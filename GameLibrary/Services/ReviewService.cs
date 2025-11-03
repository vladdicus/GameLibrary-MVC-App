using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Services
{
    // initiate the class using the interface
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        // retrieve average rating for a game and return it using .Average()
        public async Task<double?> GetAverageRatingAsync(int gameId)
        {
            var ratings = await _context.Reviews
                .Where(r => r.GameId == gameId)
                .Select(r => (double)r.Rating)
                .ToListAsync();

            if (ratings.Count == 0) return null;
            return ratings.Average();
        }

        // retrieve all reviews for a game, ordered by most recent (going off of review ID)
        public async Task<List<Review>> GetReviewsForGameAsync(int gameId)
        {
            return await _context.Reviews
                .Where(r => r.GameId == gameId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        // add a new review to the database
        public async Task AddReviewAsync(Review review)
        {
            // input validation for rating
            if (review.Rating < 1 || review.Rating > 5)
                throw new ArgumentOutOfRangeException(nameof(review.Rating), "Rating must be 1–5.");


            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
