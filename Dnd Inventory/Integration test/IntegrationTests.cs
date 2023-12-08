using Dnd_Inventory_DAL;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Integration_test
{
    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<dndInventoryApi>>
    {
        private readonly CustomWebApplicationFactory<dndInventoryApi> _factory;

        public IntegrationTests(CustomWebApplicationFactory<dndInventoryApi> factory)
        {
            _factory = factory;

            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var Context = scope.ServiceProvider.GetService<InventoryDbContext>();

                DatabaseSeeder.Init(Context);
                DatabaseSeeder.Seed();
            }
        }

        [Theory]
        [InlineData("/api/Session")]
        public async Task Get_SessionReturnSuccessAndCorrectValues(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("[{\"id\":1,\"name\":\"Arcus's session\",\"createdBy\":\"6565e4921336ad0c552c2ebb\",\"joinKeys\":null},{\"id\":2,\"name\":\"Kalrez's session\",\"createdBy\":\"6565e4921336ad0c552c2ebb\",\"joinKeys\":null}]", result);
        }
    }
}