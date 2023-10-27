using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record FetchPokemonCommand : IRequest
{

}

public class FetchPokemonCommandHandler : IRequestHandler<FetchPokemonCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly ILogger<FetchPokemonCommandHandler> _logger;

    public FetchPokemonCommandHandler(IDataContext context, IMapper mapper, ILogger<FetchPokemonCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(FetchPokemonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"fetchPokemon start - {DateTime.Now}");

        try
        {
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/pokemon?limit=99999999999999", cancellationToken);

            if (content is not null)
            {
                foreach (var pokemon in content.Results)
                {
                    var pokemonEntity = _mapper.Map<PokemonEntity>(pokemon);

                    var storedPokemon = await _context.Pokemons.FirstOrDefaultAsync(x => x.ExternalId == pokemonEntity.ExternalId, cancellationToken);

                    if (storedPokemon is null)
                    {
                        _context.Pokemons.Attach(pokemonEntity);
                        await _context.Pokemons.AddAsync(pokemonEntity, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        pokemonEntity.Id = storedPokemon.Id;
                        _context.Pokemons.Entry(storedPokemon).CurrentValues.SetValues(pokemonEntity);
                        _context.Pokemons.Entry(storedPokemon).State = EntityState.Modified;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"fetchPokemon exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            _logger.LogInformation($"fetchPokemon end - {DateTime.Now}");
        }
    }
}
