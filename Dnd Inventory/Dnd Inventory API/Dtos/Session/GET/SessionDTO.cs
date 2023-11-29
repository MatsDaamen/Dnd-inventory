using System.ComponentModel.DataAnnotations.Schema;

namespace Dnd_Inventory_API.Dtos.Session.GET
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }

        public List<JoinKeyDto> JoinKeys { get; set; }
    }
}
