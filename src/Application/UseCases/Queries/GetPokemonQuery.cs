using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Queries;

public record GetPokemonQuery : IRequest<List<PokemonEntity>>
{

}

public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, List<PokemonEntity>>
{
    private readonly IDataContext _context;

    public GetPokemonQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<List<PokemonEntity>> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
    {
        return await _context.Pokemons
           .Include(p => p.Sprites)
           .Include(p => p.Types)
           .Include(p => p.PokemonDetail)
           .AsNoTracking()
           .OrderBy(p => p.ExternalId)
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
