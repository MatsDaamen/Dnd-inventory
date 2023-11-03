namespace Dnd_Inventory_Logic.DomainModels
{
    public class SessionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public List<SessionJoinKeyModel> SessionJoinKeys { get; set; }
    }
}
