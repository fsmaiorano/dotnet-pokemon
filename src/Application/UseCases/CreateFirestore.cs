using Application.Common.Interfaces;
using Application.Common.Models;
using Application.UseCases.Queries;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record CreateFirestoreCommand : IRequest
{

}

public class CreateFirestoreCommandHandler : IRequestHandler<CreateFirestoreCommand>
{
    private readonly ILogger<CreateFirestoreCommandHandler> _logger;
    private readonly IFirestoreContext _firestoreContext;
    private readonly IDataContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateFirestoreCommandHandler(ILogger<CreateFirestoreCommandHandler> logger,
                                         IFirestoreContext firestoreContext,
                                         IDataContext context,
                                         IMediator mediator,
                                         IMapper mapper)
    {
        _logger = logger;
        _firestoreContext = firestoreContext;
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Handle(CreateFirestoreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"createFirestore start - {DateTime.Now}");

            //Get all pokemons from database using query command
            //save all pokemons in firestore

            var getPokemonQuery = new GetPokemonQuery();
            var pokemons = await _mediator.Send(getPokemonQuery, cancellationToken);


            //convert to pokemon model


            var p = _mapper.Map<List<Pokemon>>(pokemons);

            // var firestoreContext = await _firestoreContext.SavePokemon();

            _logger.LogInformation($"createFirestore end - {DateTime.Now}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"createFirestore error - {DateTime.Now}");
            throw;
        }
    }
}
