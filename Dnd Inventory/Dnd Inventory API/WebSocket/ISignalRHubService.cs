using Dnd_Inventory_API.Dtos.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Inventory_API.WebSocket
{
    public interface ISignalRHubService
    {
        public Task UpdateInventory(InventoryDto inventory);
    }
}
