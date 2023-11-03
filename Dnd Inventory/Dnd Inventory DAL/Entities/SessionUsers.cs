using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dnd_Inventory_DAL.Entities
{
    public class SessionUsers
    {

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }

        [Key]
        public int UserId { get; set; }
    }
}
