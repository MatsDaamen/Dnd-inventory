using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class InventoryRepository : IInventoryRepository
    {
        private SessionDbContext _db;

        public InventoryRepository(SessionDbContext db) 
        {
            _db = db;
        }

        public List<InventoryModel> GetInventory(string userId, int sessionId)
        {
            List<Inventory> inventory = _db.SessionUsers
                .Include(item => item.Items)
                .Where(item => item.SessionId == sessionId && item.UserId == userId)
                .Select(item => new Inventory
                {
                    UserId = userId,
                    SessionId = sessionId,
                    Items = item.Items
                })
                .ToList();

            List<InventoryModel> inventoryModel = inventory.Select(inventoryItem => new InventoryModel
            {
                UserId = inventoryItem.UserId,
                SessionId = inventoryItem.SessionId,
                itemModels = inventoryItem.Items.Select(item => new ItemModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Price = item.Price,
                    Weight = item.Weight,
                    sessionId = item.SessionId,
                    Amount = _db.inventories.First(inventory => 
                    inventory.ItemId == item.Id && 
                    inventory.UserId == inventoryItem.UserId &&
                    inventory.SessionId == inventoryItem.SessionId).Amount
                }).ToList()
            }).ToList();

            return inventoryModel;
        }

        public InventoryModel GetSessionInventory(int sessionId)
        {
            //Inventory inventory = _db.inventories
            //    .Include(item => item.Items)
            //    .First(item => item.SessionId == sessionId && string.IsNullOrEmpty(item.UserId));

            //InventoryModel inventoryModel = new InventoryModel
            //{
            //    UserId = inventory.UserId,
            //    SessionId = inventory.SessionId,
            //    itemModels = inventory.Items.Select(item => new ItemModel
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Description = item.Description,
            //        Type = item.Type,
            //        Price = item.Price,
            //        Weight = item.Weight,
            //        sessionId = item.SessionId,
            //        Amount = inventory.Amount
            //    }).ToList()
            //};

            //return inventoryModel;
            return null;
        }

        public List<InventoryModel> GetAllBySessionId(int sessionId)
        {
            List<Inventory> inventory = _db.SessionUsers
                .Include(item => item.Items)
                .Where(item => item.SessionId == sessionId)
                .Select(item => new Inventory
                {
                    UserId = item.UserId,
                    SessionId = sessionId,
                    Items = item.Items
                })
                .ToList();

            List<Inventory> inv = _db.inventories.ToList();

            List<InventoryModel> inventoryModel = inventory.Select(inventoryItem => new InventoryModel
            {
                UserId = inventoryItem.UserId,
                SessionId = inventoryItem.SessionId,
                itemModels = inventoryItem.Items.Select(item => new ItemModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Price = item.Price,
                    Weight = item.Weight,
                    sessionId = item.SessionId,
                    Amount = _db.inventories.First(inventory =>
                        inventory.ItemId == item.Id &&
                        inventory.UserId == inventoryItem.UserId &&
                        inventory.SessionId == inventoryItem.SessionId).Amount
                }).ToList()
            }).ToList();

            return inventoryModel;
        }

        public void Create(int itemId, int sessionId, string userId, int amount = 1)
        {
            Inventory? inventory = _db.inventories.FirstOrDefault(inventory => inventory.ItemId == itemId && inventory.SessionId == sessionId && inventory.UserId == userId);

            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ItemId = itemId,
                    SessionId = sessionId,
                    UserId = userId,
                    Amount = amount
                };

                _db.inventories.Add(inventory);
            }
            else
            {
                inventory.Amount += amount;
                inventory.UserId = userId;

                if (inventory.Amount <= 0)
                    Delete(userId, sessionId, itemId);
            }

            _db.SaveChanges();
        }

        public void Delete(string userId, int sessionId, int itemId)
        {
            Inventory inventory = _db.inventories.First(inventory => inventory.ItemId == itemId && inventory.SessionId == sessionId && inventory.UserId == userId);
            _db.Remove(inventory);
            _db.SaveChanges();
        }
    }
}
