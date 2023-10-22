using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record FetchAbilityCommand : IRequest
{

}

public class FetchAbilityCommandHandler : IRequestHandler<FetchAbilityCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly ILogger<FetchAbilityCommandHandler> _logger;

    public FetchAbilityCommandHandler(IDataContext context, IMapper mapper, ILogger<FetchAbilityCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(FetchAbilityCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"fetchAbility start - {DateTime.Now}");

        try
        {
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/ability?limit=9999999", cancellationToken);

            if (content is not null)
            {
                foreach (var ability in content.Results)
                {
                    var abilityEntity = _mapper.Map<AbilityEntity>(ability);

                    var storedAbility = await _context.Abilities.FirstOrDefaultAsync(x => x.ExternalId == abilityEntity.ExternalId, cancellationToken);

                    if (storedAbility is null)
                    {
                        _context.Abilities.Attach(abilityEntity);
                        await _context.Abilities.AddAsync(abilityEntity, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        abilityEntity.Id = storedAbility.Id;
                        _context.Abilities.Entry(storedAbility).CurrentValues.SetValues(abilityEntity);
                        _context.Abilities.Entry(storedAbility).State = EntityState.Modified;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"fetchAbility exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            _logger.LogInformation($"fetchAbility end - {DateTime.Now}");
        }
    }
}
