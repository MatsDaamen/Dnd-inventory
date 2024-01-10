namespace Dnd_Inventory_API.Dtos.Session.GET
{
    public class JoinKeyDto
    {
        public int Id { get; set; }

        public Guid JoinKey { get; set; }

        public int UsesLeft { get; set; }

        public int SessionId { get; set; }
    }
}
