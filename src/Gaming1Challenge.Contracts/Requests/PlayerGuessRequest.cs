using System.ComponentModel.DataAnnotations;

namespace Gaming1Challenge.Contracts.Requests;

public class PlayerGuessRequest
{
    [Required(ErrorMessage = "The field playerId is required.")]
    [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use default Guid")]
    public Guid PlayerId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer (e.g. 7)")]
    public int PlayerGuessNumber { get; set; }
}
