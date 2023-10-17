using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases;

public record FetchAbilityCommand : IRequest
{

}

public class FetchAbilityCommandHandler : IRequestHandler<FetchAbilityCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchAbilityCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(FetchAbilityCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"fetchAbility start - {DateTime.Now}");

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
            Console.WriteLine($"fetchAbility exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"fetchAbility end - {DateTime.Now}");
        }
    }
}
