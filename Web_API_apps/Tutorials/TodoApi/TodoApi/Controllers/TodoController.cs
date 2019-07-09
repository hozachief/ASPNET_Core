/** 
 * -Defines an Application Programming Interface (API) controller class without methods.
 * -Decorates the class with the [ApiController] attribute. This attribute indicates
 * that the controller responds to web API requests.
 * -Uses dependency injection (DI) to inject the database context (TodoContext) into
 * the controller.
 * -Adds an item named Item1 to the database if the database is empty. This code
 * is in the constructor, so it runs every time there's a new HTTP request. If you
 * delete all items, the constructor creates Item1 again the next time an
 * Application Programming Interface (API) method is called.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
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
                // which means you cannot delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        /** 
         * GET: "endpoint"
         * Test the app by calling the two endpoints from a browser. For example
         * -https://localhost:<port>/api/todo
         * -https://localhost:<port>/api/todo/1
         * 
         * [HttpGet] attribute denotes a method that responds to an HTTP GET request.
         * The URL path for each method is constructed as follows:
         * -Start with the template string in the controller's Route attribute
         * (i.e. [Route("api/[controller]")]).
         * -Replace [controller] with the name of the controller minus the "Controller"
         * suffix. For example, the controller class name is TodoController, so the
         * controller name is "todo".
         * -If the [HttpGet] attribute has a route template (e.g. [HttpGet("products")]),
         * append that to the path.
         * 
         * The return type of the GetTodoItems and GetTodoItem methods is ActionResult<T>
         * type. ASP.NET Core automatically serializes the object to JSON and writes
         * the JSON into the body of the response message. The response code for
         * this return type is 200, assuming there are no unhandled exceptions.
         * Unhandled exceptions are translated into 5xx errors.
         * 
         * ActionResult return types can represent a wide range of HTTP status codes.
         * For example, GetTodoItem can return two different status values:
         * -If no item matches the requested ID, the method returns a 404 NotFound error code.
         * -Otherwise, the method returns 200 with a JSON response body. Returning
         * item results in an HTTP 200 response.        
        */
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        /** 
         * "{id}" is a placeholder variable for the unique identifier of the to-do
         * item. When GetTodoItem is invoked, the value of "{id}" in the URL is
         * provided to the method in its id parameter.        
        */
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

        /** 
         * HTTP POST method, as indicated by the [HttpPost] attribute. The method
         * gets the value of the to-do item from the body of the HTTP request.
         * 
         * The CreatedAtAction method:
         * -Returns an HTTP 201 status code, if successful. HTTP 201 is the standard
         * response for an HTTP POST method that creates a new resource on the server.
         * -Adds a Location header to the response. The Location header specifies
         * the URI of the newly created to-do item.
         * -References the GetTodoItem action to create the Location header's URI.
         * The C# nameof keyword is used to avoid hard-coding the action name
         * in the CreatedAtAction call.        
        */
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        /** 
         * Similiar to PostTodoItem, except it uses HTTP PUT. The response is
         * 204 (No Content). According to the HTTP specification, a PUT request
         * requires the client to send the entire updated entity, not just the
         * changes. To support partial updates, use HTTP PATCH.
        */
        // PUT: api/Todo/5
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

        /** 
         * The DeleteTodoItem response is 204 (No Content)
         * The sample app allows you to delete all the items, but when the last
         * item is deleted, a new one is created by the model class constructor
         * the next time the Application Programming Interface (API) is called.
        */
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
