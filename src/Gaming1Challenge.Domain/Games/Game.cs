using Gaming1Challenge.Domain.Players;

namespace Gaming1Challenge.Domain.Games;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int MisteryNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IList<Guid> PlayersId { get; set; } = [];

    public int GenerateMisteryNumber()
    {
        Random random = new Random();
        MisteryNumber = random.Next(0, 100);
        return MisteryNumber;
    }

    public bool IsGameActive()
    {
        return IsActive;
    }

    public void TurnGameInactive()
    {
        IsActive = false;
    }

    public bool PlayerGuessNumberIsCorrect(int playerGuessNumber)
    {
        return playerGuessNumber == MisteryNumber;
    }

    public bool IsPlayerExist(Guid playerId)
    {
        return PlayersId.Contains(playerId);
    }
}
