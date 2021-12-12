using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace Tests.Helpers.Fake
{
    public class TodoRepositoryFake : ITodoRepository
    {
        private int _nextId = 2;
        private readonly List<TodoItem> _items = new List<TodoItem>
        {
            new TodoItem
            {
                Id = 0,
                Title = "First test",
                Description = "First test description",
                IsComplete = false
            },
            new TodoItem
            {
                Id = 1,
                Title = "Second test",
                Description = "Second test description",
                IsComplete = true
            }
        };
        
        public Task<IEnumerable<TodoItem>> GetAllItemsAsync()
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public Task<TodoItem> GetItemByIdAsync(int id)
        {
            return Task.FromResult(_items.FirstOrDefault(i => i.Id == id));
        }

        public Task<TodoItem> AddItemAsync(TodoItem item)
        {
            var newItem = new TodoItem
            {
                Id = _nextId++
            };
            newItem.ModifyFrom(item);
            _items.Add(newItem);
            return Task.FromResult(newItem);
        }

        public async Task<TodoItem> ModifyItemAsync(TodoItem item)
        {
            var searchedItem = await GetItemByIdAsync(item.Id);
            if (searchedItem == null) return null;
            searchedItem.ModifyFrom(item);
            return searchedItem;
        }

        public async Task<bool> DeleteItemWithIdAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            return _items.Remove(item);
        }
    }
}