using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record FetchDetailCommand : IRequest<PokemonDetail?>
{
    public int PokemonExternalId { get; init; }
}

public class FetchDetailCommandHandler : IRequestHandler<FetchDetailCommand, PokemonDetail?>
{
    private readonly IDataContext _context;
    private readonly ILogger<FetchDetailCommandHandler> _logger;

    public FetchDetailCommandHandler(IDataContext context, ILogger<FetchDetailCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PokemonDetail?> Handle(FetchDetailCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"fetchDetail start - {DateTime.Now}");

        PokemonDetail? detail = new();

        try
        {
            var pokemon = await HttpHelper.GetAsync<PokemonDetail>($"https://pokeapi.co/api/v2/pokemon/{request.PokemonExternalId}", cancellationToken);

            if (pokemon is not null)
            {
                detail.ExternalId = request.PokemonExternalId;
                detail.Sprites = pokemon.Sprites;
                detail.Height = pokemon.Height;
                detail.Weight = pokemon.Weight;
                detail.Types = pokemon.Types;
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"fetchDetail exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            _logger.LogInformation($"fetchDetail end - {DateTime.Now}");
        }

        return detail;
    }
}
