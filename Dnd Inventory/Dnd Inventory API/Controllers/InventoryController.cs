using Dnd_Inventory_API.Dtos.Inventory;
using Dnd_Inventory_API.Dtos.Item;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnd_Inventory_API.Controllers
{
    [Authorize("read:inventory")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController
    {
        private IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService) 
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("{sessionId}/{userId}")]
        public List<InventoryDto> Get(string userId, int sessionId)
        {
            List<InventoryModel> inventoryModel = _inventoryService.Get(userId, sessionId);

            List<InventoryDto> inventoryDto = inventoryModel.Select(inventoryItem => new InventoryDto
            {
                SessionId = inventoryItem.SessionId,
                UserId = inventoryItem.UserId,
                Items = inventoryItem.itemModels.Select(item => new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Weight = item.Weight,
                    Price = item.Price,
                    SessionId = item.sessionId
                }).ToList()
            }).ToList();

            return inventoryDto;
        }

        [HttpGet("{sessionId}")]
        public List<InventoryDto> Get(int sessionId)
        {
            List<InventoryModel> inventoryModel = _inventoryService.GetAll(sessionId);

            List<InventoryDto> inventoryDto = inventoryModel.Select(inventoryItem => new InventoryDto
            {
                SessionId = inventoryItem.SessionId,
                UserId = inventoryItem.UserId,
                Items = inventoryItem.itemModels?.Select(item => new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Weight = item.Weight,
                    Price = item.Price,
                    SessionId = item.sessionId
                }).ToList()
            }).ToList();

            return inventoryDto;
        }

        [HttpPost]
        public void Post(AddItemRequest request)
        {
                _inventoryService.AddItem(request.ItemId, request.SessionId, request.UserId, request.Amount);
        }

        [HttpPost("transfer")]
        public void Post(TransferItemRequest request)
        {
            _inventoryService.TransferItem(request.ItemId, request.SessionId, request.UserId, request.NewUserId, request.Amount);
        }

        [HttpDelete("{sessionId}/{userId}/{itemId}")]
        public void Delete(int sessionId, string userId, int itemId)
        {
            _inventoryService.DeleteItem(itemId, sessionId, userId);
        }
    }
}
