using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases;

public record FetchPokemonCommand : IRequest
{

}

public class FetchPokemonCommandHandler : IRequestHandler<FetchPokemonCommand>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchPokemonCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(FetchPokemonCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"fetchPokemon start - {DateTime.Now}");

        try
        {
            var content = await HttpHelper.GetAsync<GenericResponse>("https://pokeapi.co/api/v2/pokemon?limit=99999999", cancellationToken);

            if (content is not null)
            {
                _context.Pokemons.AddRange(_mapper.Map<List<PokemonEntity>>(content.Results));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"fetchPokemon exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"fetchPokemon end - {DateTime.Now}");
        }
    }
}
