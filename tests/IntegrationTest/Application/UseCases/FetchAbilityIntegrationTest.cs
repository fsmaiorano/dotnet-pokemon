namespace IntegrationTest.Application.UseCases;

[TestClass]
public class FetchAbilityIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public FetchAbilityIntegrationTest(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task FetchAbilityCommand_ShouldReturnTrue()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/ability");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("true", responseString);
    }
}