using Dapper;
using Gaming1Challenge.Application.Interfaces.Repositories;
using Gaming1Challenge.Application.Interfaces.Repositories.Data;
using Gaming1Challenge.Domain.Games;
using Gaming1Challenge.Infrastructure.Data;

namespace Gaming1Challenge.Infrastructure.Repositories;

public class GamesRepository : IGamesRepository
{
    private readonly DbSession _dbSession;

    private readonly IUnitOfWork _unitOfWork;

    public GamesRepository(DbSession dbSession, IUnitOfWork unitOfWork)
    {
        _dbSession = dbSession;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Game>> GetAsync()
    {
        _unitOfWork.BeginTransaction();

        var games = await _dbSession.Connection.ExecuteScalarAsync<IReadOnlyList<Game>>(DatabaseDml.SelectAllGames);

        return games!;
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        _unitOfWork.BeginTransaction();

        var game = await _dbSession.Connection.QuerySingleOrDefaultAsync<Game>(DatabaseDml.SelectGameById,
            new {
                Id = id
            });

        return game!;
    }

    private async Task<List<Guid>> PlayersNotExists(IList<Guid> playersId)
    {
        var players = new List<Guid>();

        foreach (var playerId in playersId)
        {
            var isPlayerExists = await _dbSession.Connection.ExecuteScalarAsync<bool>(DatabaseDml.SelectIfPlayerAlreadyExistsInPlayersTable,
                new {
                    Id = playerId
                });

            if (!isPlayerExists)
            {
                players.Add(playerId);
            }
        }

        return players;
    }

    private async Task SavePlayers(IList<Guid> playersId)
    {
        _unitOfWork.BeginTransaction();

        var players = await PlayersNotExists(playersId);

        var recordsInserted = 0;

        foreach (var playerId in players)
        {
            recordsInserted = await _dbSession.Connection.ExecuteAsync(DatabaseDml.InsertIntoPlayersTable,
                new {
                    Id = playerId,
                    Name = $"PlayerName {Guid.NewGuid()}",
                    Timestamp = DateTime.UtcNow
                });
        }

        if (recordsInserted > 0)
        {
            _unitOfWork.Commit();
        }
        else
        {
            _unitOfWork.Dispose();
        }
    }

    public async Task<Game> SaveAsync(Game game)
    {
        await SavePlayers(game.PlayersId);
        
        _unitOfWork.BeginTransaction();

        await _dbSession.Connection.ExecuteAsync(DatabaseDml.InsertIntoGamesTable,
            new {
                Id = game.Id,
                MisteryNumber = game.MisteryNumber,
                IsActive = game.IsActive,
                Timestamp = game.Timestamp
            });

        foreach (var playerId in game.PlayersId)
        {
            await _dbSession.Connection.ExecuteAsync(DatabaseDml.InsertIntoGamesPlayersTable,
                new {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.UtcNow,
                    GameId = game.Id,
                    PlayerId = playerId
                });
        }

        _unitOfWork.Commit();

        return game;
    }

    public async Task<Game> TurnGameInactive(Game game)
    {
        _unitOfWork.BeginTransaction();

        await _dbSession.Connection.ExecuteAsync(DatabaseDml.TurnGameInactive,
            new {
                Id = game.Id
            });

        _unitOfWork.Commit();

        return game;
    }

    public async Task<bool> IsPlayerInTheGame(Guid playerId, Guid gameId)
    {
        _unitOfWork.BeginTransaction();

        var isPlayerInTheGame = await _dbSession.Connection.QueryFirstOrDefaultAsync<bool>(DatabaseDml.SelectPlayerInTheGame, 
            new {
                PlayerId = playerId,
                GameId = gameId
            });

        _unitOfWork.Dispose();

        return isPlayerInTheGame;
    }
}
