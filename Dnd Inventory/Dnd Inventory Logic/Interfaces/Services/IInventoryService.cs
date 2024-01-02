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
        public InventoryModel GetAll(string userId);

        public void AddItem(int itemId, string userId, int amount = 1);

        public void RemoveItem(int itemId, string userId, int amount = 1);
    }
}
