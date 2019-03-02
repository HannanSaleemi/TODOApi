using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

// Creating a Controller
// Defones an API controller without models
// Decorates the class with the [ApiController] attribute. This attribute indicates that the controller responds to web API requests.
// Uses DI to inject the database context (TodoContext) into the controller. The database context is used in each of the CRUD methods in the controller
// Adds an item named Item1 to the database if the database is empty. The code is in the constructor is run eveyrtime there is a new HTTP request.
//      If you delete all items, the constructor creates Item1 again the next time an API method is called. So it may look like the deletion didn't work when it actually did.

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // whcih means you can't delete all TodoItems
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        // ADD GET METHODS
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
    }
}