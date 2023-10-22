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

    public HandlePokemonCommandHandler(IDataContext context, IMapper mapper, IServiceProvider serviceProvider, ILogger<HandlePokemonCommandHandler> logger)
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
                    var typeEntity = new List<TypeEntity>();
                    var types = pokemon.Types?.Select(x => x.Type).ToList();
                    if (types is not null)
                    {
                        typeEntity = types
                            .Select(type => new TypeEntity
                            {
                                ExternalId = int.Parse(type!.Url!.Split('/').Reverse().Skip(1).First() ?? "0"),
                                Name = type.Name!,
                                Url = type.Url!
                            }).ToList();
                    }

                    var pokemonEntity = new PokemonEntity()
                    {
                        ExternalId = pokemon.Id,
                        Url = $"https://pokeapi.co/api/v2/pokemon/{pokemon.Id}",
                        Name = pokemon.Name!,
                        // Sprites = spriteEntity,
                        // PokemonDetail = pokemonDetailEntity
                        Types = typeEntity,
                        // Abilities = _mapper.Map<List<AbilityEntity>>(pokemon.Abilities),w
                        // Moves = _mapper.Map<List<MoveEntity>>(pokemon.Moves),
                        // Height = pokemon.Height,
                        // Weight = pokemon.Weight,
                        // EvolvesFrom = pokemon.EvolvesFrom
                    };

                    var storedPokemon = await _context.Pokemons.Where(x => x.ExternalId == pokemonEntity.ExternalId)
                                                               .Include(x => x.Types).FirstOrDefaultAsync(cancellationToken);

                    if (storedPokemon is null)
                    {
                        _context.Pokemons.Attach(pokemonEntity);
                        await _context.Pokemons.AddAsync(pokemonEntity, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        storedPokemon.Types ??= new List<TypeEntity>();

                        pokemonEntity.Id = storedPokemon.Id;
                        _context.Pokemons.Entry(storedPokemon).CurrentValues.SetValues(pokemonEntity);

                        storedPokemon.Types.Clear();
                        storedPokemon.Types.AddRange(typeEntity);

                        _context.Pokemons.Entry(storedPokemon).State = EntityState.Modified;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
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
