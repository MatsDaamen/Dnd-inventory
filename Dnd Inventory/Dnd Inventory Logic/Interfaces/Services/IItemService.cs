using Dnd_Inventory_Logic.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Services
{
    public interface IItemService
    {
        public ItemModel Get(int id);

        public List<ItemModel> GetAll(int sessionId);

        public void Create(ItemModel item);
    }
}
