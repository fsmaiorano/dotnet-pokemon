## About

This project is a micro api application using clean architecture and CQRS pattern, that allows get pokemons from [PokeApi](https://pokeapi.co/) and save in a database.

## Architecture
Clean architecture is used to separate the application into layers. The layers are as follows: application, domain, and infrastructure. The application layer contains use-cases and all core implementations. The domain layer contains the business logic and
models. The infrastructure layer contains the database context and migrations.

The implementation between the Api and the rest of the application is built with CQRS (Command and Query Responsibility Segregation), a pattern that separates read and update operations for a data store.


## Technologies Used
- .NET 7
- ASP.NET MVC
- Entity Framework Core with InMemory Database
- C#


## How to run

### Requirements
- .NET 7
- Docker

### Steps
1. Clone this repository
2. Run the command `docker-compose up` in the root folder
3. Open the browser and go to `http://localhost:port/swagger/index.html` to see the swagger documentation
4. Use the endpoints to get pokemons from [PokeApi](https://pokeapi.co/) and save in a database
5. Create a firestore database, download the credentials file and put in the root folder with the name `firebase.json`

## Endpoints
- See client.http file in the root folder

## Tests
- Run the command `dotnet test` in the root folder

### To-do
- [ ] Implement cache to improve performance
- [x] Implement pagination
- [ ] Implement unit tests
- [x] Implement integration tests
- [x] Implement docker
- [x] Implement swagger
- [ ] Implement logging with serilog
- [x] Implement error handling


