using Dnd_Inventory_Logic.DomainModels;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface IItemRepository
    {
        public ItemModel Get(int id);
        public List<ItemModel> GetAllSessionItems(int sessionId);
        public List<ItemModel> GetAll();
        public void Create(ItemModel itemModel);
    }
}
