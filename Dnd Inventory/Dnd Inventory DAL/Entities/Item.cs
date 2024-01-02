using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_DAL.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; }
        public int Price { get; set; }

        [ForeignKey("SessionId")]
        public int? SessionId { get; set; }
    }
}
