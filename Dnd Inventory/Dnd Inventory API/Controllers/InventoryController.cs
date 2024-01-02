using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnd_Inventory_API.Controllers
{
    [Authorize("read:inventory")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController
    {
        
    }
}
