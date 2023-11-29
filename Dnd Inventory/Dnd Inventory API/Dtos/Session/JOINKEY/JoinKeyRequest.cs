namespace Dnd_Inventory_API.Dtos.Session.JOINKEY
{
    public class JoinKeyRequest
    {
        public int sessionId { get; set; }

        public int amountOfUses { get; set; }
        
        public string createdBy { get; set; }
    }
}
