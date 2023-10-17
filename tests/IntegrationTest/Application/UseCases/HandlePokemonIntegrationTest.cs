using Application;
using Application.UseCases;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchHandlePokemonIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchHandlePokemon()
    {
        var command = new HandlePokemonCommand
        {

        };

        await SendAsync(command);
    }
}