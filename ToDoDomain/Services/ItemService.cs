using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDataAccess.Repository;
using ToDoDataAccess.Models;
using ToDoDomain.Models;

namespace ToDoDomain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item> CreateItemAsync(CreateItemDto newItem)
        {
            var itemMapped = MapNewItem(newItem);

            var item = await _itemRepository.CreateAsync(itemMapped);

            if (item == null)
                throw new Exception($"A new item was not created");

            return item;
        }

        public async Task<ReadItemDto> ReadItemByIdAsync(int id)
        {
            var item = await _itemRepository.ReadAsync(id);

            if (item == null)
                throw new Exception($"Item with id {id} not found");

            var mappedItem = MapToDisplayStatus(item);

            return mappedItem;
        }

        public async Task<Item> UpdateItemAsync(ItemDto itemUpdates)
        {
            var item = await _itemRepository.ReadAsync(itemUpdates.Id);

            if (item == null)
                throw new Exception($"Item with id {itemUpdates.Id} not found");

            item.Title = itemUpdates.Title;
            item.Description = itemUpdates.Description;

            var updatedItem = await _itemRepository.UpdateAsync(item);
            return updatedItem;
        }
 
        public async Task DeleteItemAsync(int id)
        {
            var item = await _itemRepository.ReadAsync(id);
            await _itemRepository.DeleteAsync(item);
        }

        public async Task<IEnumerable<ReadItemDto>> GetAllTasksAsync()
        {
            var items = await _itemRepository.GetAllAsync();

            var mappedItems = items.Select(item => MapToDisplayStatus(item)).ToArray();

            return mappedItems;
        }

        public async Task<Item> MarkItemAsDone(int id)
        {
            var item = await _itemRepository.ReadAsync(id);
            item.Status = Status.Done;
            await _itemRepository.UpdateAsync(item);
            return item;
        }

        internal Item MapNewItem(CreateItemDto unmappedItem)
        {
            return new Item
            {
                Title = unmappedItem.Title,
                Description = unmappedItem.Description,
                Status = Status.InProgress
            };
        }

        internal Item MapItem(ItemDto unmappedItem, Item item)
        {
            return new Item
            {
                Id = unmappedItem.Id,
                Title = unmappedItem.Title,
                Description = unmappedItem.Description,
                Status = item.Status,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate
            };
        }

        internal ReadItemDto MapToDisplayStatus(Item item)
        {
            return new ReadItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Status = Enum.GetName(typeof(Status), item.Status),
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate
            };
        }
    }
}
