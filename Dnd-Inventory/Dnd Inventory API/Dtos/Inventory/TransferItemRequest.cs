namespace Dnd_Inventory_API.Dtos.Inventory
{
    public class TransferItemRequest
    {
        public int SessionId { get; set; }
        public string UserId { get; set; }
        public string NewUserId { get; set; }
        public int ItemId { get; set; }

        public int Amount { get; set; }
    }
}
