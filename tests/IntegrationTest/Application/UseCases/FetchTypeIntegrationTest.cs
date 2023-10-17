using Application;
using Application.UseCases;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchTypeIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchType()
    {
        var command = new FetchTypeCommand
        {

        };

        await SendAsync(command);
        var counter = await CountAsync<TypeEntity>();
        Assert.IsTrue(counter > 0);
    }
}