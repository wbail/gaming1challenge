namespace Gaming1Challenge.Application.Interfaces;

public interface IPlayersService
{
    Task<bool> IsPlayerExistAsync(Guid playerId);
    Task<int> GetPlayerIterationsAsync(int playerGuessNumber);
}
