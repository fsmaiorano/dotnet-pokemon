using Application.UseCases;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class CreateFirestoreIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task CreateFirestore()
    {
        // var jsonOptions = new JsonSerializerOptions
        // {
        //     PropertyNameCaseInsensitive = true,
        //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //     DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        // };

        // var solutionPath = PathHelper.GetSolutionPath();
        // var file = Path.Combine(solutionPath.FullName, "tests", "IntegrationTest", "DataTest", "HandlePokemonResponse.json");

        // var content = await File.ReadAllTextAsync(file, cancellationToken);
        // var pokemons = JsonSerializer.Deserialize<List<PokemonEntity>>(content, jsonOptions);

        // foreach (var pokemon in pokemons!)
        // {
        //     var storedPokemon = await _context.Pokemons.FirstOrDefaultAsync(x => x.ExternalId == pokemon.ExternalId, cancellationToken);

        //     if (storedPokemon is null)
        //     {
        //         _context.Pokemons.Attach(pokemon);
        //         await _context.Pokemons.AddAsync(pokemon, cancellationToken);
        //         await _context.SaveChangesAsync(cancellationToken);
        //     }
        //     else
        //     {
        //         pokemon.Id = storedPokemon.Id;
        //         _context.Pokemons.Entry(storedPokemon).CurrentValues.SetValues(pokemon);
        //         _context.Pokemons.Entry(storedPokemon).State = EntityState.Modified;
        //         await _context.SaveChangesAsync(cancellationToken);
        //     }
        // }


        var command = new CreateFirestoreCommand
        {

        };

        await SendAsync(command);
    }
}