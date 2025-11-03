using GameLibrary.Models;

namespace GameLibrary.Services
{
    public interface IReviewService
    {
        // create a method to get average rating for a game
        Task<double?> GetAverageRatingAsync(int gameId);

        // create a method to get all reviews for a game
        Task<List<Review>> GetReviewsForGameAsync(int gameId);

        // create a method to add a new review
        Task AddReviewAsync(Review review);
    }
}
