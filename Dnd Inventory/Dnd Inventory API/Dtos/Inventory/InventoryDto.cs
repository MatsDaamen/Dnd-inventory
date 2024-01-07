using Dnd_Inventory_API.Dtos.Item;

namespace Dnd_Inventory_API.Dtos.Inventory
{
    public class InventoryDto
    {
        public string UserId { get; set; }
        public int SessionId { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
