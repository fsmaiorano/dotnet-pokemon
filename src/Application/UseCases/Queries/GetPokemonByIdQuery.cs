using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application;

public record GetPokemonByIdQuery : IRequest<PokemonEntity?>
{
    public int ExternalId { get; set; }
}

public class GetPokemonByIdQueryHandler : IRequestHandler<GetPokemonByIdQuery, PokemonEntity?>
{
    private readonly IDataContext _context;

    public GetPokemonByIdQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<PokemonEntity?> Handle(GetPokemonByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Pokemons
             .Include(p => p.Sprites)
             .Include(p => p.Types)
             .Include(p => p.PokemonDetail)
             .AsNoTracking()
             .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken: cancellationToken);
    }
}
