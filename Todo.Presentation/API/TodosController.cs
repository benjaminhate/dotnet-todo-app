using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain;

namespace Todo.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : Controller
    {
        private readonly ITodoRepository _repository;

        public TodosController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItem>))]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _repository.GetAllItemsAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItem))]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _repository.GetItemByIdAsync(id);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }
    }
}