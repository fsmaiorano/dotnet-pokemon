using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record HandlePokemonCommand : IRequest<List<Pokemon>>
{

}

public class HandlePokemonCommandHandler : IRequestHandler<HandlePokemonCommand, List<Pokemon>>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<HandlePokemonCommandHandler> _logger;

    public HandlePokemonCommandHandler(IDataContext context,
                                       IMapper mapper, IServiceProvider serviceProvider,
                                       ILogger<HandlePokemonCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<List<Pokemon>> Handle(HandlePokemonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"handlePokemon start - {DateTime.Now}");

        List<Pokemon> pokemons = new();

        try
        {
            await FetchInformation();

            var storedPokemons = await _context.Pokemons.ToListAsync(cancellationToken);

            var batches = storedPokemons.Select((x, i) => new { Index = i, Value = x })
                                        .GroupBy(x => x.Index / 100)
                                        .Select(x => x.Select(v => v.Value).ToList())
                                        .ToList();

            var tasks = batches.Select(x => HandlePokemon(x, cancellationToken));

            var results = await Task.WhenAll(tasks);

            pokemons = results.SelectMany(x => x).ToList();

            if (pokemons.Any())
            {
                foreach (var pokemon in pokemons)
                {
                    var pokemonEntity = await _context.Pokemons.FirstOrDefaultAsync(x => x.ExternalId == pokemon.Id, cancellationToken);

                    if (pokemonEntity is null)
                        break;

                    var storedPokemon = await _context.Pokemons.Where(x => x.ExternalId == pokemonEntity.ExternalId)
                                                               .Include(x => x.Types)
                                                               .Include(x => x.Abilities)
                                                               .Include(x => x.Moves)
                                                               .Include(x => x.Sprites)
                                                               .FirstOrDefaultAsync(cancellationToken);

                    if (storedPokemon is null)
                        break;

                    storedPokemon.Types ??= new List<TypeEntity>();

                    pokemonEntity.Id = storedPokemon.Id;
                    storedPokemon.PokemonDetail = new PokemonDetailEntity
                    {
                        ExternalId = storedPokemon.ExternalId,
                        PokemonId = storedPokemon.Id,
                        Height = pokemon.Height,
                        Weight = pokemon.Weight,
                        EvolvesFromPokemonExternalId = int.Parse(pokemon.EvolvesFrom?.ToString() ?? "0")
                    };

                    var storedPokemonDetail = await _context.Details.FirstOrDefaultAsync(x => x.PokemonId == storedPokemon.Id, cancellationToken);
                    if (storedPokemonDetail is null)
                        await _context.Details.AddAsync(storedPokemon.PokemonDetail, cancellationToken);
                    else
                        storedPokemonDetail = storedPokemon.PokemonDetail;

                    storedPokemon.Sprites = new SpriteEntity
                    {
                        ExternalId = pokemon.Id,
                        PokemonId = pokemonEntity.Id,
                        BackDefault = pokemon.Sprites?.BackDefault,
                        BackFemale = pokemon.Sprites?.BackFemale,
                        BackShiny = pokemon.Sprites?.BackShiny,
                        BackShinyFemale = pokemon.Sprites?.BackShinyFemale,
                        FrontDefault = pokemon.Sprites?.FrontDefault,
                        FrontFemale = pokemon.Sprites?.FrontFemale,
                        FrontShiny = pokemon.Sprites?.FrontShiny,
                        FrontShinyFemale = pokemon.Sprites?.FrontShinyFemale,
                        DreamWorldFrontDefault = pokemon.Sprites?.Others?.DreamWorld?.FrontDefault,
                        DreamWorldFrontFemale = pokemon.Sprites?.Others?.DreamWorld?.FrontFemale,
                        HomeFrontDefault = pokemon.Sprites?.Others?.Home?.FrontDefault,
                        HomeFrontFemale = pokemon.Sprites?.Others?.Home?.FrontFemale,
                        HomeFrontShiny = pokemon.Sprites?.Others?.Home?.FrontShiny,
                        HomeFrontShinyFemale = pokemon.Sprites?.Others?.Home?.FrontShinyFemale,
                        OfficialArtworkFrontDefault = pokemon.Sprites?.Others?.OfficialArtwork?.FrontDefault,
                        OfficialArtworkFrontShiny = pokemon.Sprites?.Others?.OfficialArtwork?.FrontShiny
                    };

                    var storedSprites = await _context.Sprites.FirstOrDefaultAsync(x => x.PokemonId == storedPokemon.Id, cancellationToken);
                    if (storedSprites is null)
                        await _context.Sprites.AddAsync(storedPokemon.Sprites, cancellationToken);
                    else
                        storedSprites = storedPokemon.Sprites;

                    var types = pokemon.Types?.Select(x => x.Type).ToList();
                    if (types is not null && types.Any())
                    {
                        foreach (var type in types)
                        {
                            var externalId = int.Parse(type!.Url!.Split('/').Reverse().Skip(1).First() ?? "0");
                            var storedType = await _context.Types.FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);

                            if (storedType is null)
                                break;

                            storedPokemon.Types.Add(storedType);
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            _logger.LogInformation($"handlePokemon success - {DateTime.Now}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"handlePokemon exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            _logger.LogInformation($"handlePokemon end - {DateTime.Now}");
        }

        return pokemons;
    }

    private async Task<List<Pokemon>> HandlePokemon(List<PokemonEntity> pokemonEntities, CancellationToken cancellationToken)
    {
        List<Pokemon> pokemons = new();

        foreach (var pokemon in pokemonEntities)
        {
            var sender = _serviceProvider.GetService<ISender>() ?? throw new ArgumentNullException(nameof(ISender));

            var fetchSpecieCommand = new FetchSpecieCommand();
            var specie = await sender.Send(new FetchSpecieCommand { PokemonExternalId = pokemon.ExternalId }, cancellationToken);

            var fetchPokemonDetailCommand = new FetchDetailCommand { PokemonExternalId = pokemon.ExternalId };
            var pokemonDetail = await sender.Send(fetchPokemonDetailCommand, cancellationToken);

            if (pokemonDetail is null)
                break;

            if (specie is not null)
                pokemonDetail.EvolvesFrom = specie?.EvolvesFromSpecies?.ExternalId ?? 0;

            var pokemonDto = new Pokemon
            {
                Id = pokemon.ExternalId,
                Name = pokemon.Name,
                Height = pokemonDetail.Height,
                Weight = pokemonDetail.Weight,
                EvolvesFrom = pokemonDetail.EvolvesFrom,
                Sprites = pokemonDetail.Sprites,
                Types = pokemonDetail.Types,
            };

            pokemons.Add(pokemonDto);
        }

        return pokemons;
    }

    private async Task FetchInformation()
    {
        var sender = _serviceProvider.GetService<ISender>() ?? throw new ArgumentNullException(nameof(ISender));

        var fetchAbilityCommand = new FetchAbilityCommand();
        await sender.Send(fetchAbilityCommand);

        var fetchMoveCommand = new FetchMoveCommand();
        await sender.Send(fetchMoveCommand);

        var fetchTypeCommand = new FetchTypeCommand();
        await sender.Send(fetchTypeCommand);

        var fetchPokemonCommand = new FetchPokemonCommand();
        await sender.Send(fetchPokemonCommand);
    }
}
