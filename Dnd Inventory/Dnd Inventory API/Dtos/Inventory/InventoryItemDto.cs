namespace Dnd_Inventory_API.Dtos.Inventory
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int? SessionId { get; set; }
    }
}
