using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit_Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void TestGetAllInventoryItems()
        {
            var InventoryStub = new Mock<IInventoryRepository>();
            var SessionStub = new Mock<ISessionRepository>();
            var sessionUserStub = new Mock<ISessionUsersRepository>();
            var joinKeyStub = new Mock<IJoinKeyRepository>();

            int sessionId = 1;

            List<InventoryModel> expectedModels = new List<InventoryModel>
            {
                new InventoryModel
                {
                    SessionId = sessionId,
                    UserId = "user 1",
                    itemModels = new List<ItemModel>
                    {
                        new ItemModel
                        {
                            Id = 1,
                            sessionId = sessionId,
                            Name = "name",
                            Description = "description",
                            Type = "type",
                            Amount = 100,
                            Weight = 100,
                            Price = 100
                        }
                    }
                },
                new InventoryModel
                {
                    SessionId = sessionId,
                    UserId = "user 2",
                    itemModels = new List<ItemModel>
                    {
                        new ItemModel
                        {
                            Id = 1,
                            sessionId = sessionId,
                            Name = "name",
                            Description = "description",
                            Type = "type",
                            Amount = 100,
                            Weight = 100,
                            Price = 100
                        }
                    }
                }
            };

            ISessionService sessionService = new SessionService(SessionStub.Object, joinKeyStub.Object, sessionUserStub.Object);

            InventoryStub.Setup(x => x.GetAllBySessionId(sessionId)).Returns(expectedModels);

            IInventoryService inventoryService = new InventoryService(InventoryStub.Object, sessionService);

            List<InventoryModel> inventoryModels = inventoryService.GetAll(sessionId);

            Assert.AreEqual(expectedModels, inventoryModels);
        }

        [TestMethod]
        public void TestGetInventoryItems()
        {
            var InventoryStub = new Mock<IInventoryRepository>();
            var SessionStub = new Mock<ISessionRepository>();
            var sessionUserStub = new Mock<ISessionUsersRepository>();
            var joinKeyStub = new Mock<IJoinKeyRepository>();

            int sessionId = 1;
            string userId = "userId";

            List<InventoryModel> expectedModels = new List<InventoryModel>
            {
                new InventoryModel
                {
                    SessionId = sessionId,
                    UserId = userId,
                    itemModels = new List<ItemModel>
                    {
                        new ItemModel
                        {
                            Id = 1,
                            sessionId = sessionId,
                            Name = "name",
                            Description = "description",
                            Type = "type",
                            Amount = 100,
                            Weight = 100,
                            Price = 100
                        },

                        new ItemModel
                        {
                            Id = 2,
                            sessionId = sessionId,
                            Name = "name",
                            Description = "description",
                            Type = "type",
                            Amount = 100,
                            Weight = 100,
                            Price = 100
                        },
                        new ItemModel
                        {
                            Id = 3,
                            sessionId = sessionId,
                            Name = "name",
                            Description = "description",
                            Type = "type",
                            Amount = 100,
                            Weight = 100,
                            Price = 100
                        }
                    }
                }
            };

            ISessionService sessionService = new SessionService(SessionStub.Object, joinKeyStub.Object, sessionUserStub.Object);

            InventoryStub.Setup(x => x.GetInventory(userId, sessionId)).Returns(expectedModels);

            IInventoryService inventoryService = new InventoryService(InventoryStub.Object, sessionService);

            List<InventoryModel> inventoryModels = inventoryService.Get(userId, sessionId);

            Assert.AreEqual(expectedModels, inventoryModels);
        }
    }
}
