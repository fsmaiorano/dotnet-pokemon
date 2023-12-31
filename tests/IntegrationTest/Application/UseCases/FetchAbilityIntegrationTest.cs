using Application;
using Application.UseCases;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchAbilityIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchAbility()
    {
        var command = new FetchAbilityCommand
        {

        };

        await SendAsync(command);
        var counter = await CountAsync<AbilityEntity>();
        Assert.IsTrue(counter > 0);
    }
}