using Microsoft.AspNetCore.Mvc.Testing;

namespace Integration_test
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<dndInventoryApi>>
    {
        private readonly WebApplicationFactory<dndInventoryApi> _factory;

        public IntegrationTests(WebApplicationFactory<dndInventoryApi> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Session/1")]
        public async Task Get_SessionReturnSuccessAndCorrectValues(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("{\"id\":1,\"name\":\"test12321\",\"createdBy\":10,\"joinKeys\":[]}", result);
        }
    }
}