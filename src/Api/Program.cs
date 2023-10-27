using System.Text.Json.Serialization;
using Application;
using Application.UseCases;
using Application.UseCases.Queries;
using Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var mediator = app.Services.GetService(typeof(ISender)) as ISender ?? throw new NullReferenceException("Mediator is null");

app.MapGet("/seed", async () =>
{
    var command = new HandlePokemonCommand
    {

    };

    await mediator.Send(command);
    return Results.Ok();

}).WithName("Root").WithOpenApi();

app.MapGet("/seed-firestore", async () =>
{
    var command = new CreateFirestoreCommand
    {

    };

    await mediator.Send(command);
    return Results.Ok();

}).WithName("CreateFirestore").WithOpenApi();

app.MapGet("/pokemon", async () =>
{
    var query = new GetPokemonQuery
    {

    };

    var response = await mediator.Send(query);
    return Results.Ok(response);

}).WithName("GetPokemon").WithOpenApi();

// "/pokemon/{pageNumber}/{pageSize}"
app.MapGet("/pokemonWithPagination", async (int? pageNumber, int? pageSize) =>
{
    var query = new GetPokemonWithPaginationQuery
    {
        PageNumber = pageNumber ?? 1,
        PageSize = pageSize ?? 10
    };

    var response = await mediator.Send(query);
    return Results.Ok(response);

}).WithName("GetPokemonWithPagination").WithOpenApi();

app.Run();

public partial class Program { }