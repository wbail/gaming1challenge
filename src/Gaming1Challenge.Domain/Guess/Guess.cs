using Gaming1Challenge.Domain.Players;

namespace Gaming1Challenge.Domain.Guesses;

public class Guess
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Number { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public Player Player { get; set; } = null!;
}
