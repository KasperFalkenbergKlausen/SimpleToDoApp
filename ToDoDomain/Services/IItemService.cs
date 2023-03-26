using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDataAccess.Models;
using ToDoDomain.Models;

namespace ToDoDomain.Services
{
    public interface IItemService
    {
        Task<Item> CreateItemAsync(CreateItemDto item);
        Task<ReadItemDto> ReadItemByIdAsync(int id);
        Task<Item> UpdateItemAsync(ItemDto itemUpdates);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<ReadItemDto>> GetAllTasksAsync();
        Task<Item> MarkItemAsDone(int id);
    }
}
