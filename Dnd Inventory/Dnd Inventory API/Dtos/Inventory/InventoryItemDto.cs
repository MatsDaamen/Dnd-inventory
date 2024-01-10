using Newtonsoft.Json;

namespace Dnd_Inventory_API.Dtos.Inventory
{
    public class InventoryItemDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("weight")]
        public float Weight { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("sessionId")]
        public int? SessionId { get; set; }
    }
}
