using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record FetchMoveCommand : IRequest
{

}

public class FetchMoveCommandHandler : IRequestHandler<FetchMoveCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly ILogger<FetchMoveCommandHandler> _logger;

    public FetchMoveCommandHandler(IDataContext context, IMapper mapper, ILogger<FetchMoveCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(FetchMoveCommand request, CancellationToken cancellationToken)
    {
         _logger.LogInformation($"fetchMove start - {DateTime.Now}");

        try
        {
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/move?limit=9999999", cancellationToken);

            if (content is not null)
            {
                _context.Moves.AddRange(_mapper.Map<List<MoveEntity>>(content.Results));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
             _logger.LogInformation($"fetchMove exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
             _logger.LogInformation($"fetchMove end - {DateTime.Now}");
        }
    }
}
