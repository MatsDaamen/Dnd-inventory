using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.DomainModels
{
    public class InventoryModel
    {
        public string UserId { get; set; }

        public List<ItemModel> itemModels { get; set; }
    }
}
