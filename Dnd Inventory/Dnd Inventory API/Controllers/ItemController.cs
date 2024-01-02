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
        private IItemService _itemService;

        public ItemController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [HttpGet]
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
        public List<ItemDto> GetAll() 
        {
            List<ItemModel> items = _itemService.GetAll();

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

        public void Create(ItemDto itemDto)
        {
            ItemModel itemModel = new ItemModel()
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Description = itemDto.Description,
                Price = itemDto.Price,
                Weight = itemDto.Weight,
                Type = itemDto.Type
            };

            _itemService.Create(itemModel);
        }
    }
}
