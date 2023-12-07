## Motivation

This project is a minimal api application using clean architecture and CQRS pattern, that allows get pokemons from [PokeApi](https://pokeapi.co/) and save in a database.
The WebUI renders a grid with the pokemons using infinite scroll. The user can click on a pokemon to see more details.

This is a simple project to show how to use clean architecture and CQRS pattern.

## Architecture

Clean architecture is used to separate the application into layers. The layers are as follows: application, domain, and infrastructure. The application layer contains use-cases and all core implementations. The domain layer contains the business logic and
models. The infrastructure layer contains the database context and migrations.

The implementation between the Api and the rest of the application is built with CQRS (Command and Query Responsibility Segregation), a pattern that separates read and update operations for a data store.

## Technologies Used
- C#
- NET 8
- Entity Framework Core with InMemory Database
- MediatR
- AutoMapper
- Docker Compose
- Swagger
- Firestore
- Angular 17

## How to run

### Requirements
- NET 8
- Docker
- EF CLI - dotnet tool install --global dotnet-ef

### Steps
1. Clone this repository
2. Run the command `docker-compose up` in the root folder
3. Go to Infrastructure folder and run the command `dotnet ef database update` to create the database
4. Open the browser and go to `http://localhost:port/swagger/index.html` to see the swagger documentation
5. Use the endpoints to get pokemons from [PokeApi](https://pokeapi.co/) and save in a database

### Seed
1. Execute Seed endpoint to get pokemons from [PokeApi](https://pokeapi.co/) and save in a database

## Endpoints
- See client.http file in the root folder

## Tests
- Run the command `dotnet test` in the root folder

### To-do
- [ ] Implement cache to improve performance - get all pokemons
- [x] Implement pagination
- [ ] Implement unit tests
- [x] Implement integration tests
- [x] Implement docker
- [x] Implement swagger
- [ ] Implement logging with serilog
- [x] Implement error handling




