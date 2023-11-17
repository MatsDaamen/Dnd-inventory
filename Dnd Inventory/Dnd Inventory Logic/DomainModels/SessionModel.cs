namespace Dnd_Inventory_Logic.DomainModels
{
    public class SessionModel
    {
        public int Id { get; }
        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public List<SessionJoinKeyModel> SessionJoinKeys { get; set; }

        public SessionModel(int id, string name, int createdBy) 
        {
            Id = id;
            Name = name;
            CreatedBy = createdBy;
        }

        public SessionModel(string name, int createdBy) 
        {
            Name = name;
            CreatedBy = createdBy;
        }
    }
}
