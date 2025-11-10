using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Services
{
    // handle game data logic such as retrieving, creating, updating, and deleting games
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        // get all games and order them by title for the Index page
        public async Task<List<Game>> GetAllAsync()
        {
            return await _context.Games
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .ToListAsync();
        }

        // get a single game by its ID for the Details page
        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        // create a new game record and save it to the database
        public async Task<Game> CreateAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        // update an existing game and save any changes
        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        // delete a game record by its ID
        public async Task DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}
