namespace Dnd_Inventory_Logic.DomainModels
{
    public class SessionJoinKeyModel
    {
        public int Id { get; set; }

        public Guid JoinKey { get; set; }

        public int UsesLeft { get; set; }

        public int SessionId { get; set; }
    }
}
