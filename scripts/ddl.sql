
CREATE DATABASE Gaming1Challenge;

CREATE TABLE IF NOT EXISTS Games (
    Id UUID,
    MisteryNumber INT NOT NULL,
    IsActive BOOLEAN,
    Timestamp timestamp NOT NULL,
    PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS Players (
    Id UUID,
    Name VARCHAR(256) NOT NULL,
    Timestamp timestamp NOT NULL,
    PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS Guesses (
    Id UUID,
    PlayerGuessNumber INT NOT NULL,
    Timestamp timestamp NOT NULL,
    PlayerId UUID,
    PRIMARY KEY(Id),
    CONSTRAINT fk_guesses_player FOREIGN KEY(PlayerId) REFERENCES Players(Id)
);

CREATE TABLE IF NOT EXISTS GamesPlayers (
    Id UUID,
    Timestamp timestamp NOT NULL,
    GameId UUID,
    PlayerId UUID,
    PRIMARY KEY(Id),
    CONSTRAINT fk_gamesplayers_game FOREIGN KEY(GameId) REFERENCES Games(Id),
    CONSTRAINT fk_gamesplayers_player FOREIGN KEY(PlayerId) REFERENCES Players(Id)
);
