using Newtonsoft.Json;

namespace Dnd_Inventory_API.Dtos.Inventory
{
    public class InventoryDto
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("sessionId")]
        public int SessionId { get; set; }
        [JsonProperty("items")]
        public List<InventoryItemDto> Items { get; set; }
    }
}
