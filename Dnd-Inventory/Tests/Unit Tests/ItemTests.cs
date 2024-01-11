using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Moq;

namespace Tests
{
    [TestClass]
    public class ItemTests
    {
        private IItemService _itemService;

        [TestInitialize]
        public void initialization()
        {

        }

        [TestMethod]
        public void testIfItemsAreFilteredSessionId() 
        { 
            var itemRepositoryMock = new Mock<IItemRepository>();

            int sessionId = 1;

            itemRepositoryMock.Setup(x => x.GetAllSessionItems(sessionId)).Returns(new List<ItemModel>
            {
                new ItemModel
                {
                    Id = 1,
                    Name = "Test 1",
                    Description = "Test",
                    Type = "Test",
                    Weight = 1,
                    Price = 1,
                    sessionId = sessionId
                },
                new ItemModel
                {
                    Id = 2,
                    Name = "Test 2",
                    Description = "Test",
                    Type = "Test",
                    Weight = 1,
                    Price = 1,
                    sessionId = sessionId
                }
            });
            itemRepositoryMock.Setup(x => x.GetAll()).Returns(new List<ItemModel>
            {
                new ItemModel
                {
                    Id = 1,
                    Name = "Test 1",
                    Description = "Test",
                    Type = "Test",
                    Weight = 1,
                    Price = 1,
                    sessionId = sessionId
                },
                new ItemModel
                {
                    Id = 2,
                    Name = "Test 2",
                    Description = "Test",
                    Type = "Test",
                    Weight = 1,
                    Price = 1,
                    sessionId = sessionId
                },
                new ItemModel
                {
                    Id = 3,
                    Name = "Test 3",
                    Description = "Test",
                    Type = "Test",
                    Weight = 1,
                    Price = 1,
                    sessionId = sessionId + 1
                },
                new ItemModel
                {
                    Id = 4,
                    Name = "Test 4",
                    Description = "Test",
                    Type = "Test",
                    Weight = 1,
                    Price = 1,
                    sessionId = sessionId + 1
                }
            });

            _itemService = new ItemService(itemRepositoryMock.Object);

            List<ItemModel> itemModels = _itemService.GetAll(sessionId);

            Assert.IsTrue(itemModels.All(x => x.sessionId == sessionId));
        }
    }
}
