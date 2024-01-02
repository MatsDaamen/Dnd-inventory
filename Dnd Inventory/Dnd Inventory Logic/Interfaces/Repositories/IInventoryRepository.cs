using Dnd_Inventory_Logic.DomainModels;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        public InventoryModel Get(int id);
        public List<InventoryModel> GetAll();

        public void Create(InventoryModel inventory);

        public void Update(InventoryModel inventory);

        public void Delete(string userId, int itemId);
    }
}
