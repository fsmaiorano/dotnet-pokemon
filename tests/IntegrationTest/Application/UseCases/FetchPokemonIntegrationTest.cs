using Application;
using Application.UseCases;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchPokemonIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchPokemon()
    {
        var command = new FetchPokemonCommand
        {

        };

        await SendAsync(command);
        var counter = await CountAsync<PokemonEntity>();
        Assert.IsTrue(counter > 0);
    }
}