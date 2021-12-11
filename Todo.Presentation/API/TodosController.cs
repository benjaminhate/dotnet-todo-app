using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Domain;

namespace Todo.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : Controller
    {
        private readonly ILogger<TodosController> _logger;
        private readonly ITodoRepository _repository;

        public TodosController(
            ITodoRepository repository,
            ILogger<TodosController> logger)
        {
            _repository = repository;
            _logger = logger;
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItem))]
        public async Task<IActionResult> ModifyTodo(int id, [FromBody] TodoItem item)
        {
            var todo = await _repository.ModifyItemAsync(item);
            if (todo == null)
                return BadRequest();
            return Ok(todo);
        }
    }
}