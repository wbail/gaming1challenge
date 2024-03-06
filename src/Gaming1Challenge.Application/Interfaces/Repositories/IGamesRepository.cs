using Gaming1Challenge.Domain.Games;

namespace Gaming1Challenge.Application.Interfaces.Repositories;

public interface IGamesRepository
{
    Task<IReadOnlyList<Game>> GetAsync();
    Task<Game> SaveAsync(Game game);
    Task<Game> GetByIdAsync(Guid id);
}
