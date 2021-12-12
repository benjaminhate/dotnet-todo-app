using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public class TodoMockRepository : ITodoRepository
    {
        private int _nextId = 3;
        private readonly List<TodoItem> _items = new List<TodoItem>
        {
            new TodoItem
            {
                Id = 0,
                IsComplete = false,
                Title = "My first Todo item",
                Description = "Description of the first todo"
            },
            new TodoItem
            {
                Id = 1,
                IsComplete = false,
                Title = "Task I should do but never will",
                Description = "This is an impossible task"
            },
            new TodoItem
            {
                Id = 2,
                IsComplete = true,
                Title = "Already completed task",
                Description = "I'm the best !"
            }
        };

        private int GetNextId()
        {
            return _nextId++;
        }

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
                Id = GetNextId()
            };
            newItem.ModifyFrom(item);
            
            _items.Add(newItem);
            return Task.FromResult(newItem);
        }

        public async Task<TodoItem> ModifyItemAsync(TodoItem item)
        {
            var searchedItem = await GetItemByIdAsync(item.Id);

            if (searchedItem == null)
                return null;
            
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