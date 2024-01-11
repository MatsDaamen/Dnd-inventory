namespace Dnd_Inventory_API.Dtos.Item
{
    public class ItemCreationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public int? SessionId { get; set; }
    }
}
