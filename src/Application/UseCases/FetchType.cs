using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record FetchTypeCommand : IRequest
{

}

public class FetchTypeCommandHandler : IRequestHandler<FetchTypeCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly ILogger<FetchTypeCommandHandler> _logger;

    public FetchTypeCommandHandler(IDataContext context, IMapper mapper, ILogger<FetchTypeCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(FetchTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"fetchType start - {DateTime.Now}");

        try
        {
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/type?limit=9999999", cancellationToken);

            if (content is not null)
            {
                _context.Types.AddRange(_mapper.Map<List<TypeEntity>>(content.Results));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"fetchType exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            _logger.LogInformation($"fetchType end - {DateTime.Now}");
        }
    }
}
