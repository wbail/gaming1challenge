using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Domain.Guesses;
using Gaming1Challenge.Domain.Players;

namespace Gaming1Challenge.Infrastructure.Services;

public class PlayersService : IPlayersService
{
    private readonly Player _player;

    public PlayersService(Player player)
    {
        _player = player;
    }

    public Task<int> GetPlayerIterationsAsync(int playerGuessNumber)
    {
        var guess = new Guess()
        {
            Number = playerGuessNumber,
            Player = _player
        };

        _player!.Guesses!.Add(guess);

        var playerIteractions = _player!.Guesses!.Where(g => g.Player.Id == _player.Id).Count();

        return Task.FromResult(playerIteractions);
    }

    public Task<bool> IsPlayerExistAsync(Guid playerId)
    {
        return Task.FromResult(_player.Id == playerId);
    }
}
