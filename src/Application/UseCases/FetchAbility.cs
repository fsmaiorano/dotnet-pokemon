using System.Text.Json;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application;

public record FetchAbilityCommand : IRequest<bool>
{

}

public class FetchAbilityCommandHandler : IRequestHandler<FetchAbilityCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchAbilityCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(FetchAbilityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://pokeapi.co/api/v2/ability?limit=9999999", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var content = await response.Content.ReadAsStringAsync(cancellationToken) ?? string.Empty;

                if (string.IsNullOrEmpty(content))
                    return false;

                var genericContent = JsonSerializer.Deserialize<GenericResponse>(content, jsonOptions) ?? new GenericResponse();
                var abilityEntities = _mapper.Map<List<AbilityEntity>>(genericContent.Results);

                _context.Abilities.AddRange(abilityEntities);
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
