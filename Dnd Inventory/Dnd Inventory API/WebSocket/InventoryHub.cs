using Dnd_Inventory_API.Dtos.Inventory;
using Microsoft.AspNetCore.SignalR;
namespace Dnd_Inventory_API.WebSocket
{
    public class InventoryHub : Hub
    {
        private ISignalRHubService _signalRHubService;

        public InventoryHub(ISignalRHubService signalRHubService)
        {
            _signalRHubService = signalRHubService;
        }

        public void addClient(string userId, string clientId)
        {

        }
    }
}
