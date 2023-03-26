using Microsoft.EntityFrameworkCore;
using ToDoDataAccess.Context;
using ToDoDataAccess.Models;

namespace ToDoDataAccess.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly ToDoContext _context;

        public ItemRepository(ToDoContext context)
        {
            _context = context;
        }

        public async Task<Item> ReadAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            return item;
        }

        public async Task<Item> CreateAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            _context.SaveChanges();
            return item;
        }

        public async Task DeleteAsync(Item item)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();            
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var items = await _context.Items.ToListAsync();
            return items;
        }
    }
}
