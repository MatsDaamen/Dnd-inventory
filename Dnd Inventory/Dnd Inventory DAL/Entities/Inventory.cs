using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_DAL.Entities
{
    public class Inventory
    {
        public string UserId { get; set; }
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }

        public int Amount { get; set; }
    }
}
