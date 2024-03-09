namespace Gaming1Challenge.Infrastructure;

public static class DatabaseDml
{
    public const string InsertIntoGamesTable = "INSERT INTO Games VALUES (@Id, @MisteryNumber, @IsActive, @Timestamp)";
    public const string InsertIntoPlayersTable = "INSERT INTO Players VALUES (@Id, @Name, @Timestamp)";
    public const string InsertIntoGamesPlayersTable = "INSERT INTO GamesPlayers VALUES (@Id, @Timestamp, @GameId, @PlayerId)";
    public const string SelectAllGames = "SELECT * FROM Games";
    public const string SelectGameById = "SELECT * FROM Games WHERE Id = @Id";
    public const string SelectIfPlayerAlreadyExistsInPlayersTable = "SELECT EXISTS(SELECT 1 FROM Players WHERE Id = @Id)";
    public const string TurnGameInactive = "UPDATE Games SET IsActive = false WHERE Id = @Id";
    public const string SelectPlayerInTheGame = "SELECT EXISTS(SELECT 1 FROM GamesPlayers WHERE PlayerId = @PlayerId AND GameId = @GameId)";
}
