using Application;
using Application.UseCases;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchDetailIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task FetchDetail()
    {
        //TODO - ?
        var command = new FetchDetailCommand
        {

        };

        await SendAsync(command);
    }
}