namespace Gaming1Challenge.Contracts.Responses;

public class NewGameResponse
{
    public Guid Id { get; set; }
    public IList<Guid> PlayersId { get; set; } = [];
    public DateTime Timestamp { get; set; }
}
