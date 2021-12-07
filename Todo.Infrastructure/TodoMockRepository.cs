using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public class TodoMockRepository : ITodoRepository
    {
        public Task<IEnumerable<TodoItem>> GetAllItemsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> GetItemByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task AddItemAsync(TodoItem item)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> ModifyItemAsync(TodoItem item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteItemWithIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}