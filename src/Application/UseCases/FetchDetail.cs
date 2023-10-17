﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases;

public record FetchDetailCommand : IRequest<PokemonDetail?>
{
    public int PokemonExternalId { get; init; }
}

public class FetchDetailCommandHandler : IRequestHandler<FetchDetailCommand, PokemonDetail?>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public FetchDetailCommandHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PokemonDetail?> Handle(FetchDetailCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"fetchDetail start - {DateTime.Now}");

        PokemonDetail? detail = new();

        try
        {
            var pokemon = await HttpHelper.GetAsync<PokemonDetail>($"https://pokeapi.co/api/v2/pokemon/{request.PokemonExternalId}", cancellationToken);

            if (pokemon is not null)
            {
                detail.ExternalId = request.PokemonExternalId;
                detail.Sprites = pokemon.Sprites;
                detail.Types = await _context.Types.Where(t => t.ExternalId == request.PokemonExternalId).ToListAsync(cancellationToken);
                detail.Height = pokemon.Height;
                detail.Weight = pokemon.Weight;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"fetchDetail exception - {DateTime.Now} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"fetchDetail end - {DateTime.Now}");
        }

        return detail;
    }
}
