using Gaming1Challenge.Domain.Games;

namespace Gaming1Challenge.Domain.UnitTests.Games;

public class GamesTests
{
    [Fact]
    public void GenerateMisteryNumber_GenerateAnInt_ReturnIntegerMisteryNumber()
    {
        // arrange
        var game = new Game();

        // act
        var misteryNumber = game.GenerateMisteryNumber();

        // assert
        Assert.True(misteryNumber.GetType() == typeof(int));
    }

    [Fact]
    public void TurnGameInactive_GameIsActive_TheGameTurnsInactive()
    {
        var game = new Game();

        game.TurnGameInactive();

        Assert.False(game.IsActive);
    }

    [Fact]
    public void PlayerGuessNumberIsCorrect_GuessIsCorrect_ReturnsTrue()
    {
        var game = new Game();
        var playerGuessNumber = 70;
        game.MisteryNumber = 70;

        var result = game.PlayerGuessNumberIsCorrect(playerGuessNumber);

        Assert.True(result);
    }

    [Fact]
    public void PlayerGuessNumberIsCorrect_GuessIsIncorrect_ReturnsFalse()
    {
        var game = new Game();
        var playerGuessNumber = 70;
        game.MisteryNumber = 7;

        var result = game.PlayerGuessNumberIsCorrect(playerGuessNumber);

        Assert.False(result);
    }

    [Fact]
    public void IsPlayerExist_PlayerIsOnTheList_ReturnsTrue()
    {
        var game = new Game();
        var playerId = Guid.Parse("f7eab9f8-46fd-4517-a323-904c6ffbe0ec");

        game.PlayersId = [
            Guid.Parse("f7eab9f8-46fd-4517-a323-904c6ffbe0ec"),
            Guid.Parse("07e2a3ed-7bb5-4049-bef9-2e18676b1be3")
        ];

        var result = game.IsPlayerExist(playerId);

        Assert.True(result);
    }

    [Fact]
    public void IsPlayerExist_PlayerIsNotOnTheList_ReturnsFalse()
    {
        var game = new Game();
        var playerId = Guid.Parse("f7eab9f8-46fd-4517-a323-904c6ffbe0ec");

        game.PlayersId = [
            Guid.Parse("07e2a3ed-7bb5-4049-bef9-2e18676b1be3")
        ];

        var result = game.IsPlayerExist(playerId);

        Assert.False(result);
    }
}
