using Gaming1Challenge.Domain.Guesses;

namespace Gaming1Challenge.Domain.Players;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public IList<Guess>? Guesses { get; set; } = [];

    public bool ValidatePlayerId(Guid playerId)
    {
        return Id == playerId;
    }
}
