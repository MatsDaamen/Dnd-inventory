using Dnd_Inventory_API.Dtos.Item;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnd_Inventory_API.Controllers
{
    [Authorize("read:items")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public ItemDto Get(int id) 
        {
            ItemModel itemModel = _itemService.Get(id);

            ItemDto itemDto = new ItemDto()
            {
                Id = id,
                Name = itemModel.Name,
                Description = itemModel.Description,
                Price = itemModel.Price,
                Weight = itemModel.Weight,
                Type = itemModel.Type,
                SessionId = itemModel.sessionId
            };

            return itemDto;
        }

        [HttpGet]
        public List<ItemDto> GetAll([FromQuery]int sessionId) 
        {
            List<ItemModel> items = _itemService.GetAll(sessionId);

            List<ItemDto> itemDtos = items.Select(item => new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Weight = item.Weight,
                Type = item.Type,
                SessionId = item.sessionId
            }).ToList();

            return itemDtos;
        }

        [HttpPost]
        public void Create(ItemCreationRequest itemCreationRequest)
        {
            ItemModel itemModel = new ItemModel()
            {
                Id = itemCreationRequest.Id,
                Name = itemCreationRequest.Name,
                Description = itemCreationRequest.Description,
                Price = itemCreationRequest.Price,
                Weight = itemCreationRequest.Weight,
                Type = itemCreationRequest.Type,
                sessionId = itemCreationRequest.SessionId
            };

            _itemService.Create(itemModel);
        }
    }
}
