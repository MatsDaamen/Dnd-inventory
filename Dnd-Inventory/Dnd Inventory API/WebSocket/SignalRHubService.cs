using Dnd_Inventory_API.Dtos.Inventory;
using Microsoft.AspNetCore.SignalR;

namespace Dnd_Inventory_API.WebSocket
{
    public class SignalRHubService : ISignalRHubService
    {

        protected IHubContext<InventoryHub> _context;

        public SignalRHubService(IHubContext<InventoryHub> context)
        {
            _context = context;
        }

        public async Task UpdateInventory(List<InventoryDto> inventory)
        {
            await _context.Clients.All.SendAsync("sendUpdateInventory", Newtonsoft.Json.JsonConvert.SerializeObject(inventory));
        }
    }
}
