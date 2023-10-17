using Application;
using Application.UseCases;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchMoveIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchMove()
    {
        var command = new FetchMoveCommand
        {

        };

        await SendAsync(command);
        var counter = await CountAsync<MoveEntity>();
        Assert.IsTrue(counter > 0);
    }
}