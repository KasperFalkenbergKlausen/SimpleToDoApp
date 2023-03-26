using Microsoft.AspNetCore.Mvc;
using ToDoDataAccess.Models;
using ToDoDomain.Models;
using ToDoDomain.Services;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]    
    public class ToDoController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ToDoController(IItemService itemService)
        {
            _itemService = itemService;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllToDoItem()
        {
            var items = await _itemService.GetAllTasksAsync();

            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<Item>> GetToDoItemById(int id)
        {
            var item = await _itemService.ReadItemByIdAsync(id);

            return Ok(item);
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<Item>> Create(CreateItemDto item)
        {
            var newItem = await _itemService.CreateItemAsync(item);

            return Ok(newItem);
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<ActionResult<Item>> Update(ItemDto itemUpdates)
        {
            var item = await _itemService.UpdateItemAsync(itemUpdates);

            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Item>> MarkItemAsDone(int id)
        {
            var item = await _itemService.MarkItemAsDone(id);

            return Ok(item);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _itemService.DeleteItemAsync(id);

            return Ok();
        }
        #endregion
    }
}
