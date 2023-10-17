using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases;

public record FetchTypeCommand : IRequest
{

}

public class FetchTypeCommandHandler : IRequestHandler<FetchTypeCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchTypeCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(FetchTypeCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"fetchType start - {DateTime.Now}");

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
            Console.WriteLine($"fetchType exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"fetchType end - {DateTime.Now}");
        }
    }
}
