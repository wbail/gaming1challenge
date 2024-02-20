namespace Gaming1Challenge.Contracts.Responses;

public class PlayerGuessResponse
{
    public Guid PlayerId { get; set; }
    public int PlayerGuessNumber { get; set; }
    public int PlayerIterations { get; set; }
    public string PlayerGuessComparedWithMisteryNumber { get; set; } = "";
    public bool IsPlayerGuessCorrect { get; set; }
    public bool IsGameActive { get; set; }
}
