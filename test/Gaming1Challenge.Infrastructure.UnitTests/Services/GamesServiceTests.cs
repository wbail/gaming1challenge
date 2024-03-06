using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Application.Interfaces.Repositories;
using Gaming1Challenge.Contracts.Requests;
using Gaming1Challenge.Domain.Games;
using Gaming1Challenge.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gaming1Challenge.Infrastructure.UnitTests.Services;

public class GamesServiceTests
{
    public GamesServiceTests()
    {
        _gameMock = new Mock<Game>();
        _playersServiceMock = new Mock<IPlayersService>();
        _gamesRepositoryMock = new Mock<IGamesRepository>();
        _logger = new Mock<ILogger<GamesService>>();

        _gamesService = new GamesService(_gameMock.Object, _playersServiceMock.Object, _gamesRepositoryMock.Object, _logger.Object);
    }

    private readonly Mock<Game> _gameMock;
    private readonly Mock<IPlayersService> _playersServiceMock;
    private readonly Mock<IGamesRepository> _gamesRepositoryMock;
    private readonly Mock<ILogger<GamesService>> _logger;
    private readonly GamesService _gamesService;

    [Fact]
    public async void CreateNewGameAsync_GameIsCreatedSuccessfully_ReturnsNewGameResponse()
    {
        // arrange
        IList<Guid> playersId = [
            Guid.Parse("90640279-9461-42fc-8c24-c6888446db08"),
            Guid.Parse("e250c58d-1e7b-4347-926c-b2b4815fe72d"),
        ];

        var game = new Game()
        {
            Id = Guid.Parse("c4648709-edf3-4d61-8f60-10aca150dde5"),
            PlayersId = playersId
        };

        _gameMock.Object.Id = Guid.Parse("c4648709-edf3-4d61-8f60-10aca150dde5");

        var newGameRequest = new NewGameRequest()
        {
            PlayersId = playersId
        };

        // act
        var result = await _gamesService.CreateNewGameAsync(newGameRequest);

        // assert
        Assert.Equal(game.Id.ToString(), result.Id.ToString());
    }

    [Fact]
    public async Task IsGameActive_GameTurnsInactive_ReturnsFalse()
    {
        IList<Guid> playersId = [
            Guid.Parse("90640279-9461-42fc-8c24-c6888446db08"),
            Guid.Parse("e250c58d-1e7b-4347-926c-b2b4815fe72d"),
        ];

        var game = new Game()
        {
            Id = Guid.Parse("c4648709-edf3-4d61-8f60-10aca150dde5"),
            PlayersId = playersId
        };

        _gameMock.Object.TurnGameInactive();

        var result = await _gamesService.IsGameActive();

        Assert.False(result);
    }

    [Fact]
    public async Task PlayerGuessAsync_PlayerGuessIsCorrect_TurnGameInactive()
    {
        var playerId = Guid.Parse("90640279-9461-42fc-8c24-c6888446db08");

        var playerGuessRequest = new PlayerGuessRequest()
        {
            PlayerId = playerId,
            PlayerGuessNumber = 7
        };

        _gameMock.Object.MisteryNumber = 7;

        var result = await _gamesService.PlayerGuessAsync(playerGuessRequest);

        Assert.False(result.IsGameActive);
    }

    [Fact]
    public async Task PlayerGuessAsync_PlayerGuessIsCorrect_ReturnPlayerGuessResponseWithIsPlayerGuessCorrectTrue()
    {
        var playerId = Guid.Parse("90640279-9461-42fc-8c24-c6888446db08");

        var playerGuessRequest = new PlayerGuessRequest()
        {
            PlayerId = playerId,
            PlayerGuessNumber = 7
        };

        _gameMock.Object.MisteryNumber = 7;

        var result = await _gamesService.PlayerGuessAsync(playerGuessRequest);

        Assert.True(result.IsPlayerGuessCorrect);
    }

    [Theory]
    [InlineData(7, 9, "higher")]
    [InlineData(77, 70, "lower")]
    [InlineData(90, 90, "equal")]
    public async Task PlayerGuessAsync_PlayerGuessIsCorrect_ReturnsHiLoEqual(int misteryNumber, int playerGuessNumber, string expectedMessage)
    {
        var playerId = Guid.Parse("90640279-9461-42fc-8c24-c6888446db08");

        var playerGuessRequest = new PlayerGuessRequest()
        {
            PlayerId = playerId,
            PlayerGuessNumber = playerGuessNumber
        };

        _gameMock.Object.MisteryNumber = misteryNumber;

        var result = await _gamesService.PlayerGuessAsync(playerGuessRequest);

        Assert.Equal(expectedMessage, result.PlayerGuessComparedWithMisteryNumber);
    }
}
