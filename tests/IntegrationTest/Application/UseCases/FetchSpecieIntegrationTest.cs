using Application;
using Application.UseCases;
using Domain.Entities;

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
        //TODO - ?
        var command = new FetchSpecieCommand
        {

        };

        await SendAsync(command);
    }
}