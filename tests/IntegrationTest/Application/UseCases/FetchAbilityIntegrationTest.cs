using Application;

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

        var result = await SendAsync(command);
        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }
}