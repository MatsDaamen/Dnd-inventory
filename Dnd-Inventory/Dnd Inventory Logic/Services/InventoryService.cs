using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Dnd_Inventory_Logic.Interfaces.Services;

namespace Dnd_Inventory_Logic.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        private readonly ISessionService _sessionService;

        public InventoryService(IInventoryRepository inventoryRepository, ISessionService sessionService)
        {
            _inventoryRepository = inventoryRepository;
            _sessionService = sessionService;
        }

        public void AddItem(int itemId, int sessionId, string userId, int amount)
        {
            if (amount == 0)
                _inventoryRepository.Create(itemId, sessionId, userId);
            else
                _inventoryRepository.Create(itemId, sessionId, userId, amount);
        }

        public void DeleteItem(int itemId, int sessionId, string userId)
        {
            _inventoryRepository.Delete(userId, sessionId, itemId);
        }

        public List<InventoryModel> Get(string userId, int sessionId)
        {
            List<InventoryModel> inventoryModel = _inventoryRepository.GetInventory(userId, sessionId);

            return inventoryModel;
        }

        public List<InventoryModel> GetAll(int sessionId)
        {
            List<InventoryModel> inventoryModel = _inventoryRepository.GetAllBySessionId(sessionId);

            if(inventoryModel.Count <= 0) 
            {
                List<SessionUserModels> users = _sessionService.GetSessionUsers(sessionId);

                foreach (SessionUserModels sessionUser in users)
                {
                    inventoryModel.Add(new InventoryModel()
                    {
                        SessionId = sessionUser.SessionId,
                        UserId = sessionUser.UserId,
                    });
                }
            }

            return inventoryModel;
        }

        public void RemoveItem(int itemId, int sessionId, string userId, int amount = 1)
        {
            _inventoryRepository.Create(itemId, sessionId, userId, -amount);
        }

        public void TransferItem(int itemId, int sessionId, string userId, string NewUserId, int amount = 1)
        {
            // remove from old owner
            _inventoryRepository.Create(itemId, sessionId, userId, -amount);

            // add to new owner
            _inventoryRepository.Create(itemId, sessionId, NewUserId, amount);
        }
    }
}
