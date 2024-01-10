using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.DomainModels
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int? sessionId { get; set; }
    }
}
