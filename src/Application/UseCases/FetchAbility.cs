using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Serilog;

namespace Application.UseCases;

public record FetchAbilityCommand : IRequest
{

}

public class FetchAbilityCommandHandler : IRequestHandler<FetchAbilityCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly ILogger _logger = Log.ForContext<FetchAbilityCommandHandler>();

    public FetchAbilityCommandHandler(IDataContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(FetchAbilityCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"Fetching abilities started at {DateTime.Now}");

        try
        {
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/ability?limit=9999999", cancellationToken);

            if (content is not null)
            {
                _context.Abilities.AddRange(_mapper.Map<List<AbilityEntity>>(content.Results));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error while fetching abilities at {DateTime.Now}");
        }
        finally
        {
            _logger.Information($"Fetching abilities completed at {DateTime.Now}");
        }
    }
}
