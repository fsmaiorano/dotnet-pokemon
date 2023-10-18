using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases;

public record HandlePokemonCommand : IRequest<List<Pokemon>>
{

}

public class HandlePokemonCommandHandler : IRequestHandler<HandlePokemonCommand, List<Pokemon>>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public HandlePokemonCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Pokemon>> Handle(HandlePokemonCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"handlePokemon start - {DateTime.Now}");

        List<Pokemon> pokemons = new();

        try
        {
            await FetchInformation();

            var storedPokemons = await _context.Pokemons.ToListAsync(cancellationToken);
            foreach (var pokemon in storedPokemons)
            {
                var specie = await new FetchSpecieCommandHandler(_context, _mapper)
                                .Handle(new FetchSpecieCommand { PokemonExternalId = pokemon.ExternalId }, cancellationToken);

                var pokemonDetail = await new FetchDetailCommandHandler(_context, _mapper)
                                .Handle(new FetchDetailCommand { PokemonExternalId = pokemon.ExternalId }, cancellationToken);

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
                    Types = pokemonDetail?.Types?.Select(x => x.Name).ToList()
                };

                pokemons.Add(pokemonDto);
            }
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

    private async Task FetchInformation()
    {
        await new FetchAbilityCommandHandler(_context, _mapper).Handle(new FetchAbilityCommand(), CancellationToken.None);
        await new FetchPokemonCommandHandler(_context, _mapper).Handle(new FetchPokemonCommand(), CancellationToken.None);
        await new FetchMoveCommandHandler(_context, _mapper).Handle(new FetchMoveCommand(), CancellationToken.None);
        await new FetchTypeCommandHandler(_context, _mapper).Handle(new FetchTypeCommand(), CancellationToken.None);
    }
}
