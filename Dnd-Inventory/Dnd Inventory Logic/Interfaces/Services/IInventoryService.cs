using Dnd_Inventory_Logic.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Services
{
    public interface IInventoryService
    {
        public List<InventoryModel> Get(string userId, int sessionId);
        public List<InventoryModel> GetAll(int sessionId);

        public void AddItem(int itemId, int sessionId, string userId, int amount);

        public void TransferItem(int itemId, int sessionId, string userId, string NewUserId, int amount = 1);

        public void RemoveItem(int itemId, int sessionId, string userId, int amount = 1);

        public void DeleteItem(int itemId, int sessionId, string userId);
    }
}
