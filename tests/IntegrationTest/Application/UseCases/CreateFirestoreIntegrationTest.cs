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
        var command = new CreateFirestoreCommand
        {

        };

        await SendAsync(command);
    }
}