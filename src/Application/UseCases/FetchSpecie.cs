using Application.Common.Models;
using Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record FetchSpecieCommand : IRequest<PokemonSpecie?>
{
    public int PokemonExternalId { get; set; }
}

public class FetchSpecieCommandHandler : IRequestHandler<FetchSpecieCommand, PokemonSpecie?>
{
    private readonly ILogger<FetchSpecieCommandHandler> _logger;

    public FetchSpecieCommandHandler(ILogger<FetchSpecieCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<PokemonSpecie?> Handle(FetchSpecieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"fetchSpecie start - {DateTime.Now}");

        PokemonSpecie? content = new();

        try
        {
            content = await HttpHelper.GetAsync<PokemonSpecie>($"https://pokeapi.co/api/v2/pokemon-species/{request.PokemonExternalId}", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"fetchSpecie exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            _logger.LogInformation($"fetchSpecie end - {DateTime.Now}");
        }

        return content;
    }
}
