using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;
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
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/pokemon?limit=99999999", cancellationToken);

            if (content is not null)
            {
                _context.Pokemons.AddRange(_mapper.Map<List<PokemonEntity>>(content.Results));
                await _context.SaveChangesAsync(cancellationToken);
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
