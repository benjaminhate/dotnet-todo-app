using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Domain
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllItemsAsync();
        
        Task<TodoItem> GetItemByIdAsync(int id);

        Task AddItemAsync(TodoItem item);

        Task<TodoItem> ModifyItemAsync(TodoItem item);

        Task<bool> DeleteItemWithIdAsync(int id);
    }
}