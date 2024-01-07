using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class ItemRepository : IItemRepository
    {
        private SessionDbContext _db;

        public ItemRepository(SessionDbContext db) 
        {
            _db = db;
        }

        public List<ItemModel> GetAll()
        {
            List<Item> items = _db.items.ToList();

            List<ItemModel> itemModels = items.Select(item => new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                Price = item.Price,
                Weight = item.Weight,
                sessionId = item.SessionId,
            }).ToList();

            return itemModels;
        }

        public ItemModel Get(int id)
        {
            Item item = _db.items.FirstOrDefault(x => x.Id == id);

            ItemModel itemModel = new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                Price = item.Price,
                Weight = item.Weight,
                sessionId = item.SessionId
            };

            return itemModel;
        }

        public void Create(ItemModel itemModel)
        {
            Item item = new Item
            {
                Id = itemModel.Id,
                Name = itemModel.Name,
                Description = itemModel.Description,
                Type = itemModel.Type,
                Price = itemModel.Price,
                Weight = itemModel.Weight,
                SessionId = itemModel.sessionId
            };

            _db.items.Add(item);
            _db.SaveChanges();
        }

        public List<ItemModel> GetAllSessionItems(int sessionId)
        {
            List<Item> items = _db.items.Where(item => item.SessionId == sessionId).ToList();

            List<ItemModel> itemModels = items.Select(item => new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                Price = item.Price,
                Weight = item.Weight,
                sessionId = item.SessionId
            }).ToList();

            return itemModels;
        }
    }
}
