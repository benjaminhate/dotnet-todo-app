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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItem>))]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = new List<TodoItem>();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItem))]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = new TodoItem();
            return Ok(todo);
        }
    }
}