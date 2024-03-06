﻿using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Application.Interfaces.Repositories;
using Gaming1Challenge.Contracts.Requests;
using Gaming1Challenge.Contracts.Responses;
using Gaming1Challenge.Domain.Games;
using Microsoft.Extensions.Logging;

namespace Gaming1Challenge.Infrastructure.Services;

public class GamesService : IGamesService
{
    private readonly Game _game;
    private readonly IPlayersService _playersService;
    private readonly IGamesRepository _gamesRepository;
    private readonly ILogger<GamesService> _logger;

    public GamesService(
        Game game,
        IPlayersService playersService,
        IGamesRepository gamesRepository,
        ILogger<GamesService> logger)
    {
        _game = game;
        _playersService = playersService;
        _gamesRepository = gamesRepository;
        _logger = logger;
    }

    public async Task<NewGameResponse> CreateNewGameAsync(NewGameRequest newGameRequest)
    {
        var newGameResponse = new NewGameResponse();

        try
        {
            _game.GenerateMisteryNumber();
    
            _game.PlayersId = newGameRequest.PlayersId;

            newGameResponse.Id = _game.Id;
            newGameResponse.PlayersId = _game.PlayersId;
            newGameResponse.Timestamp = _game.Timestamp;

            _logger.LogInformation("New Game Created: {0}", _game.Id);
            _logger.LogInformation("Mistery Number: {0}", _game.MisteryNumber);
    
            await _gamesRepository.SaveAsync(_game);

            _logger.LogInformation($"Games saved successfully into database with id {_game.Id}");

            return newGameResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error on {typeof(GamesService).Name}: {ex.Message}", ex);
            
            newGameResponse.Id = Guid.Empty;
            newGameResponse.PlayersId = _game.PlayersId;
            newGameResponse.Timestamp = _game.Timestamp;
        }

        return newGameResponse;
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
