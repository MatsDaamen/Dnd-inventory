using Dnd_Inventory_API.Dtos.Inventory;
using Microsoft.AspNetCore.SignalR;
namespace Dnd_Inventory_API.WebSocket
{
    public class InventoryHub : Hub
    {

        public InventoryHub()
        {

        }

        public void OnConnection(string userId, string clientId)
        {
            Console.WriteLine($"user: {userId} connected on: {clientId}");
        }
    }
}
