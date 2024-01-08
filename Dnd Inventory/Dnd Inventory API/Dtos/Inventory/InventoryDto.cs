namespace Dnd_Inventory_API.Dtos.Inventory
{
    public class InventoryDto
    {
        public string UserId { get; set; }
        public int SessionId { get; set; }

        public List<InventoryItemDto> Items { get; set; }
    }
}
