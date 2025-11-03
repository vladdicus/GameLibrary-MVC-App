using GameLibrary.Data;
using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IReviewService _reviewService;

        public GamesController(ApplicationDbContext context, IReviewService reviewService)
        {
            _context = context;
            _reviewService = reviewService;
        }

        // retrieve a game's details and use the service for the rest of the logic
        public async Task<IActionResult> Details(int id)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (game == null) return NotFound();

            // use service to get average rating and reviews
            var avg = await _reviewService.GetAverageRatingAsync(id);
            var reviews = await _reviewService.GetReviewsForGameAsync(id);

            ViewBag.AverageRating = avg;
            ViewBag.Reviews = reviews;

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int gameId, int rating, string? comment, string? reviewerName)
        {
            // use the service to add a review
            await _reviewService.AddReviewAsync(new Review
            {
                GameId = gameId,
                Rating = rating,
                Comment = comment,
                ReviewerName = reviewerName
            });

            return RedirectToAction(nameof(Details), new { id = gameId });
        }
    }
}
