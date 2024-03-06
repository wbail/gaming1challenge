docker build --no-cache -f .\src\Gaming1Challenge.Api\Dockerfile . -t gaming1challengedev

docker compose -f .\docker-compose.debug.yml up