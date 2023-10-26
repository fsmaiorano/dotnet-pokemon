using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using Application.UseCases.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record CreateFirestoreCommand : IRequest
{

}

public class CreateFirestoreCommandHandler : IRequestHandler<CreateFirestoreCommand>
{
    private readonly ILogger<CreateFirestoreCommandHandler> _logger;
    private readonly IFirestoreContext _firestoreContext;
    private readonly IDataContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateFirestoreCommandHandler(ILogger<CreateFirestoreCommandHandler> logger,
                                         IFirestoreContext firestoreContext,
                                         IDataContext context,
                                         IMediator mediator,
                                         IMapper mapper)
    {
        _logger = logger;
        _firestoreContext = firestoreContext;
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Handle(CreateFirestoreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"createFirestore start - {DateTime.Now}");

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            var solutionPath = PathHelper.GetSolutionPath();
            var file = Path.Combine(solutionPath.FullName, "tests", "IntegrationTest", "DataTest", "HandlePokemonResponse.json");

            var content = await File.ReadAllTextAsync(file, cancellationToken);
            var pokemons = JsonSerializer.Deserialize<List<PokemonEntity>>(content, jsonOptions);

            foreach (var pokemon in pokemons!)
            {
                var storedPokemon = await _context.Pokemons.FirstOrDefaultAsync(x => x.ExternalId == pokemon.ExternalId, cancellationToken);

                if (storedPokemon is null)
                {
                    _context.Pokemons.Attach(pokemon);
                    await _context.Pokemons.AddAsync(pokemon, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    pokemon.Id = storedPokemon.Id;
                    _context.Pokemons.Entry(storedPokemon).CurrentValues.SetValues(pokemon);
                    _context.Pokemons.Entry(storedPokemon).State = EntityState.Modified;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            var getPokemonQuery = new GetPokemonQuery();
            var storedPokemons = await _mediator.Send(getPokemonQuery, cancellationToken);

            //TODO - Configure automapper to map PokemonEntity to Pokemon
            var pokemonList = new List<Pokemon>();
            foreach (var pokemon in storedPokemons)
            {
                var p = new Pokemon
                {
                    Id = pokemon.ExternalId,
                    Name = pokemon.Name,
                    Height = pokemon.PokemonDetail!.Height,
                    Weight = pokemon.PokemonDetail!.Weight,
                    EvolvesFrom = pokemon.PokemonDetail!.EvolvesFromPokemonExternalId,
                    Sprites = new PokemonSprite
                    {
                        BackDefault = pokemon.Sprites!.BackDefault,
                        BackFemale = pokemon.Sprites.BackFemale,
                        BackShiny = pokemon.Sprites.BackShiny,
                        BackShinyFemale = pokemon.Sprites.BackShinyFemale,
                        FrontDefault = pokemon.Sprites.FrontDefault,
                        FrontFemale = pokemon.Sprites.FrontFemale,
                        FrontShiny = pokemon.Sprites.FrontShiny,
                        FrontShinyFemale = pokemon.Sprites.FrontShinyFemale,
                        Others = new PokemonSprite.Other
                        {
                            DreamWorld = new PokemonSprite.DreamWorld
                            {
                                FrontDefault = pokemon.Sprites.DreamWorldFrontDefault,
                                FrontFemale = pokemon.Sprites.DreamWorldFrontFemale
                            },
                            Home = new PokemonSprite.Home
                            {
                                FrontDefault = pokemon.Sprites.HomeFrontDefault,
                                FrontFemale = pokemon.Sprites.HomeFrontFemale,
                                FrontShiny = pokemon.Sprites.HomeFrontShiny,
                                FrontShinyFemale = pokemon.Sprites.HomeFrontShinyFemale
                            },
                            OfficialArtwork = new PokemonSprite.OfficialArtwork
                            {
                                FrontDefault = pokemon.Sprites.OfficialArtworkFrontDefault,
                                FrontShiny = pokemon.Sprites.OfficialArtworkFrontShiny
                            }
                        }
                    },
                    Types = pokemon.Types!.Select(t => new PokemonType
                    {
                        Type = new PokemonType.TypeObject
                        {
                            Name = t.Name,
                            Url = t.Url
                        }

                    }).ToList()
                };

                pokemonList.Add(p);
            }


            // var firestoreContext = await _firestoreContext.SavePokemon();

            _logger.LogInformation($"createFirestore end - {DateTime.Now}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"createFirestore error - {DateTime.Now}");
            throw;
        }
    }
}