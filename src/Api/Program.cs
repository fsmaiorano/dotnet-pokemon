using System.Text.Json.Serialization;
using Application;
using Application.UseCases;
using Application.UseCases.Queries;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server.Features;

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

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

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

app.MapGet("/pokemon", async () =>
{
    var query = new GetPokemonQuery
    {

    };

    var response = await mediator.Send(query);
    return Results.Ok(response);

}).WithName("GetPokemon").WithOpenApi();

app.MapGet("/pokemonByExternalId", async (int? externalId) =>
{
    var query = new GetPokemonByIdQuery
    {
        ExternalId = externalId ?? 1
    };

    var response = await mediator.Send(query);
    return Results.Ok(response);

}).WithName("GetPokemonByExternalId").WithOpenApi();

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

app.MapGet("/pokemonDescriptionByExternalId", async (int? externalId) =>
{
    var query = new FetchSpecieCommand
    {
        PokemonExternalId = externalId ?? 0
    };

    var response = await mediator.Send(query);
    return Results.Ok(response);

}).WithName("GetPokemonDescriptionByExternalId").WithOpenApi();

bool isFirstRun = true;

if (isFirstRun)
{
    var command = new HandlePokemonCommand
    {

    };

    await mediator.Send(command);
    isFirstRun = false;
}

app.Run();

public partial class Program { }