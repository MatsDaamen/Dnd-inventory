using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Dnd_Inventory_Logic.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Services
{
    public class ItemService : IItemService
    {
        private IItemRepository _ItemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _ItemRepository = itemRepository;
        }

        public void Create(ItemModel item)
        {
            _ItemRepository.Create(item);
        }

        public ItemModel Get(int id)
        {
            return _ItemRepository.Get(id);
        }

        public List<ItemModel> GetAll(int sessionId)
        {
            List<ItemModel> items;
            if (sessionId == 0)
                items = _ItemRepository.GetAll();
            else
                items = _ItemRepository.GetAllSessionItems(sessionId);

            return items;
        }
    }
}
