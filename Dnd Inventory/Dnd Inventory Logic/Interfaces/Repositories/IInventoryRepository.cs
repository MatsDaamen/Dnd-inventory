using Dnd_Inventory_Logic.DomainModels;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        public List<InventoryModel> GetInventory(string userId, int sessionId);
        public InventoryModel GetSessionInventory(int sessionId);
        public List<InventoryModel> GetAllBySessionId(int sessionId);

        public void Create(int itemId, int sessionId, string userId, int amount = 1);

        public void Delete(string userId, int sessionId, int itemId);
    }
}
