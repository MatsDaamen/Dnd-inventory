using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Moq;

namespace Tests.Unit_Tests
{
    [TestClass]
    public class ItemTests
    {
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

            IItemService _itemService = new ItemService(itemRepositoryMock.Object);

            List<ItemModel> itemModels = _itemService.GetAll(sessionId);

            Assert.IsTrue(itemModels.All(x => x.sessionId == sessionId));
        }

        [TestMethod]
        public void testGet()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();

            int itemId = 1;

            ItemModel expectedItem = new ItemModel
            {
                Id = itemId,
                Name = "expected",
                Description = "Test",
                Type = "Test",
                Price = 1,
                Weight = 1,
                Amount = 1
            };

            itemRepositoryMock.Setup(x => x.Get(itemId)).Returns(expectedItem);
            itemRepositoryMock.Setup(x => x.Get(itemId + 1)).Returns(new ItemModel
            {
                Id = itemId + 1,
                Name = "not expected",
                Description = "Test",
                Type = "Test",
                Price = 1,
                Weight = 1,
                Amount = 1
            });
            itemRepositoryMock.Setup(x => x.Get(itemId + 2)).Returns(new ItemModel
            {
                Id = itemId + 3,
                Name = "not expected",
                Description = "Test",
                Type = "Test",
                Price = 1,
                Weight = 1,
                Amount = 1
            });

            IItemService _itemService = new ItemService(itemRepositoryMock.Object);

            ItemModel itemModel = _itemService.Get(itemId);

            Assert.AreEqual(expectedItem, itemModel);
        }
    }
}
