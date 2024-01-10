using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dnd_Inventory_DAL.Entities
{
    public class SessionUsers
    {
        [Key]
        public int SessionId { get; set; }

        [Key]
        public string UserId { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
