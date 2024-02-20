using System.Net.Mime;
using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Contracts.Requests;
using Gaming1Challenge.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Gaming1Challenge.Api;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGamesService _gamesService;

    public GamesController(IGamesService gamesService)
    {
        _gamesService = gamesService;
    }

    [HttpPost("{id}", Name = "PlayerGuess")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlayerGuess([FromBody] PlayerGuessRequest playerGuessRequest)
    {
        var isGameActive = await _gamesService.IsGameActive();

        if (!isGameActive)
        {
            return BadRequest("Game is closed");
        }

        var isPlayerOnThisGame = await _gamesService.ValidatePlayerIdAsync(playerGuessRequest.PlayerId);

        if (!isPlayerOnThisGame)
        {
            return BadRequest("Player is not on this game.");
        }

        var playerGuessResponse = await _gamesService.PlayerGuessAsync(playerGuessRequest);

        return Created($"/games/", playerGuessResponse);
    }

    [HttpPost(Name = "NewGame")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<NewGameResponse>> NewGame([FromBody] NewGameRequest newGameRequest)
    {
        var newGameResponse = await _gamesService.CreateNewGameAsync(newGameRequest);
        return Created($"/games/{newGameResponse.Id}", newGameResponse);
    }
}
