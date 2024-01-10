using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_DAL.Entities
{
    public class Inventory
    {
        public string? UserId { get; set; }

        public int SessionId { get; set; }

        public int ItemId { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public int Amount { get; set; }
    }
}
