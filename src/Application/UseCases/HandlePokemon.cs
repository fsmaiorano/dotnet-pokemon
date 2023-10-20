using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases;

public record HandlePokemonCommand : IRequest<List<Pokemon>>
{

}

public class HandlePokemonCommandHandler : IRequestHandler<HandlePokemonCommand, List<Pokemon>>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly IServiceProvider _serviceProvider;

    public HandlePokemonCommandHandler(IDataContext context, IMapper mapper, IServiceProvider serviceProvider)
    {
        _context = context;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<Pokemon>> Handle(HandlePokemonCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"handlePokemon start - {DateTime.Now}");

        List<Pokemon> pokemons = new();

        try
        {
            await FetchInformation();

            var storedPokemons = await _context.Pokemons.ToListAsync(cancellationToken);

            // pokemons = await HandlePokemon(storedPokemons, cancellationToken);

            var batches = storedPokemons.Select((x, i) => new { Index = i, Value = x })
                                        .GroupBy(x => x.Index / 100)
                                        .Select(x => x.Select(v => v.Value).ToList())
                                        .ToList();

            var tasks = batches.Select(x => HandlePokemon(x, cancellationToken));

            var results = await Task.WhenAll(tasks);

            pokemons = results.SelectMany(x => x).ToList();

            Console.WriteLine($"handlePokemon success - {DateTime.Now}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"handlePokemon exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"handlePokemon end - {DateTime.Now}");
        }

        return pokemons;
    }

    private async Task<List<Pokemon>> HandlePokemon(List<PokemonEntity> pokemonEntities, CancellationToken cancellationToken)
    {
        List<Pokemon> pokemons = new();

        foreach (var pokemon in pokemonEntities)
        {
            var sender = _serviceProvider.GetService<ISender>() ?? throw new ArgumentNullException(nameof(ISender));

            var fetchSpecieCommand = new FetchSpecieCommandHandler();
            var specie = await fetchSpecieCommand.Handle(new FetchSpecieCommand { PokemonExternalId = pokemon.ExternalId }, cancellationToken);

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
                Types = pokemonDetail.Types?.Select(x => x.Type?.Name!).ToList() ?? new List<string>()
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
