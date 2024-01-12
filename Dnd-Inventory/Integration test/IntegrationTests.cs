using Azure;
using Dnd_Inventory_API.Dtos.Inventory;
using Dnd_Inventory_API.Dtos.Item;
using Dnd_Inventory_API.Dtos.Session.GET;
using Dnd_Inventory_API.Dtos.Session.JOIN;
using Dnd_Inventory_DAL;
using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
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
        [InlineData("/api/Session")]
        public async Task TestSessionJoining(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var userId = "8888e4921336ad0c552c2ebb";
            List<HttpResponseMessage> responses = new();
            JoinRequestDto request = new JoinRequestDto
            {
                  userId = userId,
                  sessionJoinKey = "0662f3d1-2744-43db-bf39-e941ae7a1fd4",
            };

            var httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response1 = await client.PostAsync(url + "/Join", httpContent);

            var response2 = await client.GetAsync(url + $"/{2}");

            var result = await response2.Content.ReadAsStringAsync();

            SessionDto session = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionDto>(result);

            response1.EnsureSuccessStatusCode();
            response2.EnsureSuccessStatusCode();

            Assert.Contains(session.Users, x => x.UserId == userId);
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

        [Theory]
        [InlineData("/api/inventory")]
        public async Task UpdateAndTransferIventoryItem(string invUrl)
        {
            // Arrange
            var sessionId = 2;
            var userId = "6565e4921336ad0c552c2ebb";
            var newUserId = "7777e4921336ad0c552c2ebb";
            var client = _factory.CreateClient();
            var ItemToBeAdded = new AddItemRequest
            {
                ItemId = 1,
                SessionId = sessionId,
                UserId = userId,
                Amount = 5
            };
            var itemToBeTransfered = new TransferItemRequest
            {
                ItemId = 1,
                SessionId = sessionId,
                UserId = userId,
                NewUserId = newUserId,
                Amount = 3
            };
            var ExpectedItem = new InventoryItemDto()
            {
                Id = 1,
                Name = "test item 1",
                Description = "test",
                Type = "debug",
                Price = 1,
                Weight = 1,
                Amount = 3,
                SessionId = sessionId,
            };
            var addItemContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ItemToBeAdded), Encoding.UTF8, "application/json");
            var transferItemContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(itemToBeTransfered), Encoding.UTF8, "application/json");

            // Act
            var addResponse = await client.PostAsync(invUrl, addItemContent);

            var transferResponse = await client.PostAsync(invUrl + "/transfer", transferItemContent);

            var GetNewOwnerResponse = await client.GetAsync(invUrl + $"/{sessionId}/{newUserId}");

            string JsonResult = await GetNewOwnerResponse.Content.ReadAsStringAsync();

            List<InventoryDto> result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InventoryDto>>(JsonResult);

            // Assert
            addResponse.EnsureSuccessStatusCode();
            transferResponse.EnsureSuccessStatusCode();

            Assert.Equivalent(ExpectedItem, result.First().Items.First(), strict: true);

        }
    }
}