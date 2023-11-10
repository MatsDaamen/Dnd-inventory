using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_DAL.Entities
{
    public class SessionJoinKey
    {
        public int Id { get; set; }

        public Guid JoinKey { get; set; }

        public int UsesLeft { get; set; }

        [ForeignKey("SessionId")]
        public int SessionId { get; set; }
    }
}
