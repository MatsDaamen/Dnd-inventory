using Dnd_Inventory_API.Dtos.Item;
using Dnd_Inventory_DAL;
using Dnd_Inventory_DAL.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Integration_test
{
    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<DndInventoryApi>>
    {
        private readonly CustomWebApplicationFactory<DndInventoryApi> _factory;

        public IntegrationTests(CustomWebApplicationFactory<DndInventoryApi> factory)
        {
            _factory = factory;

            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();

            if (scopeFactory is not null)
                using (var scope = scopeFactory.CreateScope())
                {
                    var Context = scope.ServiceProvider.GetService<SessionDbContext>();

                    if (Context is not null)
                    {
                        DatabaseSeeder.Init(Context);
                        DatabaseSeeder.Seed();
                    }
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
            Assert.Equal("[{\"id\":1,\"name\":\"Arcus's session\",\"createdBy\":\"6565e4921336ad0c552c2ebb\",\"joinKeys\":null,\"users\":null},{\"id\":2,\"name\":\"Kalrez's session\",\"createdBy\":\"6565e4921336ad0c552c2ebb\",\"joinKeys\":null,\"users\":null}]", result);
        }

        [Theory]
        [InlineData("/api/Session")]
        public async Task InsureSessionControllerStatusCodeSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            List<HttpResponseMessage> responses = new();
            var sessionToBeCreated = new
            {
                Name = "test",
                CreatedBy = "userId"
            };
            var httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(sessionToBeCreated), Encoding.UTF8, "application/json");

            // Act
            responses.Add(await client.GetAsync(url));
            responses.Add(await client.GetAsync(url + $"/1"));
            responses.Add(await client.PostAsync(url, httpContent));
            responses.Add(await client.DeleteAsync(url + $"/1"));

            // Assert
            for (int i = 0; i < responses.Count; i++)
            {
                try
                {
                    responses[i].EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw(new Exception($"response {i + 1} didn't have success status code", ex));
                }
            }
        }

        [Theory]
        [InlineData("/api/item")]
        public async Task CreateAndGetNewItem(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var ItemToBeCreated = new ItemDto
            {
                Name = "test",
                Description = "test",
                Type = "test",
                Weight = 1,
                Price = 1,
                SessionId = 1
            };
            var httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ItemToBeCreated), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, httpContent);
            
            var result = await response.Content.ReadAsStringAsync();

            ItemToBeCreated.Id = int.Parse(result);

            response = await client.GetAsync(url + $"/{result}");

            result = await response.Content.ReadAsStringAsync();

            string expectedResult = Newtonsoft.Json.JsonConvert.SerializeObject(ItemToBeCreated).ToLower();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResult, result.ToLower());
        }
    }
}