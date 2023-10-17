using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using MediatR;

namespace Application.UseCases;

public record FetchSpecieCommand : IRequest<PokemonSpecie?>
{
    public int PokemonExternalId { get; set; }
}

public class FetchSpecieCommandHandler : IRequestHandler<FetchSpecieCommand, PokemonSpecie?>
{
    //TODO - ?
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchSpecieCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PokemonSpecie?> Handle(FetchSpecieCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"fetchSpecie start - {DateTime.Now}");

        PokemonSpecie? content = new();

        try
        {
            content = await HttpHelper.GetAsync<PokemonSpecie>($"https://pokeapi.co/api/v2/pokemon-species/{request.PokemonExternalId}", cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"fetchSpecie exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"fetchSpecie end - {DateTime.Now}");
        }

        return content;
    }
}
