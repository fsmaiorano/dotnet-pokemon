using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Queries;

public record GetPokemonWithPaginationQuery : IRequest<PaginatedList<PokemonEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPokemonWithPaginationHandler : IRequestHandler<GetPokemonWithPaginationQuery, PaginatedList<PokemonEntity>>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public GetPokemonWithPaginationHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PokemonEntity>> Handle(GetPokemonWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Pokemons
             .Include(p => p.Sprites)
             .Include(p => p.Types)
             .Include(p => p.PokemonDetail)
             .AsNoTracking()
             .OrderBy(p => p.Name)
             .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
