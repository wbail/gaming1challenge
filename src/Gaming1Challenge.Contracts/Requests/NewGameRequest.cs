using System.Collections;

namespace Gaming1Challenge.Contracts.Requests;

public class NewGameRequest
{
    public IList<Guid> PlayersId { get; set; } = null!;
}
