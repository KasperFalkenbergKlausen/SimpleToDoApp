using ToDoDataAccess.Models;

namespace ToDoDataAccess.Repository
{
    public interface IItemRepository
    {
        Task<Item> ReadAsync(int id);
        Task<Item> CreateAsync(Item item);

        Task<Item> UpdateAsync(Item item);

        Task DeleteAsync(Item item);

        Task<IEnumerable<Item>> GetAllAsync();
    }


}
