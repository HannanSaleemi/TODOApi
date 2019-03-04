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
    [Route("api/todo")]
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

        // POST: api/Todo
        // The method gets the value of the to-do item from the body of the HTTP request

        // The CreatesAtAction method:
            // Retunrs 201 if successful
            // Adds a Location header to the responser. Specified the URL of the newly created to-do item
            // References the GetTodoItem action to create the Location header URI. The C# nameof is used to avoud hard-coding the action name in the CreatesAtAction call.
        
        // Running POST in postman - You can set body to RAW adn select JSON
        // Add the body:
        // { "name":"Walk Dog", "isComplete": true }
        // SEND
        // You will see the newly created item
        // You will also see the Location Header in the Headers tab - Pointing you back to the API response and id for that event
        // You can send a GET request to the URL with the same ID and get the newly posted infromation again
        
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT api/Todo/5
        // PUT request returnds 204 (No Content) - Used when required to update the whole entity, not just the chnages.
        // Will get error if no item in the DB

        // Testing it
        // Create new Item - Call GET
        // Then update the item with ID=1 (The one just created by the GET request)
        // URL: localhost:5001/api/todo/1
        // Body of PUT: { "ID":1, "name":"feed fish", "isComplete":true }
        // No content will be outputted.
        // Now GET Request on localhost:5001/api/todo/1 should show our new info
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}