namespace Dnd_Inventory_Logic.DomainModels
{
    public class SessionModel
    {
        public int Id { get; }
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public List<SessionJoinKeyModel> SessionJoinKeys { get; set; }

        public List<SessionUserModels> SessionUsers { get; set; }

        public SessionModel(int id, string name, string createdBy) 
        {
            Id = id;
            Name = name;
            CreatedBy = createdBy;
        }

        public SessionModel(string name, string createdBy) 
        {
            Name = name;
            CreatedBy = createdBy;
        }
    }
}
