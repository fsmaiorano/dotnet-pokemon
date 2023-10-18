using Application.UseCases;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchSpecieIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchSpecie()
    {
        var command = new FetchSpecieCommand
        {
            PokemonExternalId = 1
        };

        var specie = await SendAsync(command);
        Assert.IsTrue(specie is not null);
        Assert.IsTrue(specie?.Name == "bulbasaur");
    }
}