using GameLibrary.Models;

namespace GameLibrary.Services
{
    // define async methods that will be used for handling game data
    public interface IGameService
    {
        // get all games from the database
        Task<List<Game>> GetAllAsync();

        // get one game by its ID
        Task<Game?> GetByIdAsync(int id);

        // create a new game
        Task<Game> CreateAsync(Game game);

        // optional methods for later (edit and delete)
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
    }
}
