using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class InventoryRepository : IInventoryRepository
    {
        private SessionDbContext _db;

        public InventoryRepository(SessionDbContext db) 
        {
            _db = db;
        }

        public List<int> GetInventoryItemIds(string userId, int sessionId)
        {
            List<int> inventoryItems = _db.inventories
                .Where(item => item.SessionId == sessionId && item.UserId == userId)
                .Select(item => item.ItemId)
                .ToList();

            return inventoryItems;
        }

        public List<InventoryModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(InventoryModel inventory)
        {
            throw new NotImplementedException();
        }
        public void Create(InventoryModel inventory)
        {
            throw new NotImplementedException();
        }

        public void Delete(string userId, int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
