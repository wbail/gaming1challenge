using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Contracts.Requests;
using Gaming1Challenge.Contracts.Responses;
using Gaming1Challenge.Domain.Games;
using Microsoft.Extensions.Logging;

namespace Gaming1Challenge.Infrastructure.Services;

public class GamesService : IGamesService
{
    private readonly Game _game;
    private readonly IPlayersService _playersService;
    private readonly ILogger<GamesService> _logger;

    public GamesService(Game game, IPlayersService playersService, ILogger<GamesService> logger)
    {
        _game = game;
        _playersService = playersService;
        _logger = logger;
    }

    public Task<NewGameResponse> CreateNewGameAsync(NewGameRequest newGameRequest)
    {
        _game.GenerateMisteryNumber();

        _logger.LogInformation("New Game Created: {0}", _game.Id);
        _logger.LogInformation("Mistery Number: {0}", _game.MisteryNumber);

        _game.PlayersId = newGameRequest.PlayersId;

        var newGameResponse = new NewGameResponse()
        {
            Id = _game.Id,
            PlayersId = _game.PlayersId,
            Timestamp = _game.Timestamp,
        };

        return Task.FromResult(newGameResponse);
    }

    public async Task<PlayerGuessResponse> PlayerGuessAsync(PlayerGuessRequest playerGuessRequest)
    {
        var playerGuessNumberIsCorrect = _game.PlayerGuessNumberIsCorrect(playerGuessRequest.PlayerGuessNumber);

        if (playerGuessNumberIsCorrect)
        {
            _game.TurnGameInactive();
            _logger.LogInformation("Game with Id: {0} is inactive", _game.Id);
        }

        var playerIteractions = await _playersService.GetPlayerIterationsAsync(playerGuessRequest.PlayerGuessNumber);

        var playerGuessResponse = new PlayerGuessResponse()
        {
            PlayerId = playerGuessRequest.PlayerId,
            PlayerGuessNumber = playerGuessRequest.PlayerGuessNumber,
            IsGameActive = _game.IsGameActive(),
            IsPlayerGuessCorrect = playerGuessNumberIsCorrect,
            PlayerIterations = playerIteractions,
            PlayerGuessComparedWithMisteryNumber = PlayerGuessComparedWithMisteryNumber(playerGuessRequest.PlayerGuessNumber)
        };

        return playerGuessResponse;
    }

    public Task<bool> IsGameActive()
    {
        return Task.FromResult(_game.IsGameActive());
    }

    private string PlayerGuessComparedWithMisteryNumber(int playerGuessNumber)
    {
        if (_game.MisteryNumber > playerGuessNumber)
        {
            return "lower";
        }
        else if (_game.MisteryNumber == playerGuessNumber)
        {
            return "equal";
        }
        else
        {
            return "higher";
        }
    }

    public Task<bool> ValidatePlayerIdAsync(Guid playerId)
    {
        var isPlayerOnThisGame = _game.IsPlayerExist(playerId);

        return Task.FromResult(isPlayerOnThisGame);
    }
}
