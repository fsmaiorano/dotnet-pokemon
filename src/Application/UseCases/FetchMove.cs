using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases;

public record FetchMoveCommand : IRequest
{

}

public class FetchMoveCommandHandler : IRequestHandler<FetchMoveCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchMoveCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(FetchMoveCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"fetchMove start - {DateTime.Now}");

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
            Console.WriteLine($"fetchMove exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"fetchMove end - {DateTime.Now}");
        }
    }
}
