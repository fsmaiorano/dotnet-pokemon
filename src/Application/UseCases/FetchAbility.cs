using System.Net.Http.Json;
using System.Text.Json;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application;

public record FetchAbilityCommand : IRequest<bool>
{

}

public class FetchAbilityCommandHandler : IRequestHandler<FetchAbilityCommand, bool>
{
    private readonly IDataContext _context;

    public FetchAbilityCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(FetchAbilityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://pokeapi.co/api/v2/ability/999999999999");

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var abilities = await response.Content.ReadFromJsonAsync<List<AbilityEntity>>(jsonOptions, cancellationToken: cancellationToken);

                if (abilities is null)
                    return false;

                _context.Abilities.AddRange(abilities);
                await _context.SaveChangesAsync(cancellationToken);

            }

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to fetch ability", ex);
        }
    }
}
