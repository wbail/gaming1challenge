using Gaming1Challenge.Contracts.Requests;
using Gaming1Challenge.Contracts.Responses;

namespace Gaming1Challenge.Application.Interfaces;

public interface IGamesService
{
    Task<NewGameResponse> CreateNewGameAsync(NewGameRequest newGameRequest);
    Task<PlayerGuessResponse> PlayerGuessAsync(PlayerGuessRequest playerGuessRequest);
    Task<bool> ValidatePlayerIdAsync(Guid playerId, Guid gameId);
    Task<bool> IsGameActive();
}
