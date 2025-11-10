using GameLibrary.Data;
using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IReviewService _reviewService;

        public GameController(ApplicationDbContext context, IReviewService reviewService)
        {
            _context = context;
            _reviewService = reviewService;
        }

        // get all games for the Index page
        public async Task<IActionResult> Index()
        {
            var games = await _context.Games
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .ToListAsync();

            return View(games);
        }

        // load the Create page for adding a new game
        public IActionResult Create()
        {
            return View();
        }

        // handle form submission for creating a new game
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {
            if (!ModelState.IsValid)
            {
                // return the same view with validation messages
                return View(game);
            }

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        // handle form submission for adding a new review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int gameId, int rating, string? comment, string? reviewerName)
        {
            if (rating < 1 || rating > 5)
            {
                // reload details page if rating is invalid
                var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
                if (game == null) return NotFound();

                var avg = await _reviewService.GetAverageRatingAsync(gameId);
                var reviews = await _reviewService.GetReviewsForGameAsync(gameId);

                ViewBag.AverageRating = avg;
                ViewBag.Reviews = reviews;

                ModelState.AddModelError(nameof(rating), "Rating must be between 1 and 5.");
                return View("Details", game);
            }

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
