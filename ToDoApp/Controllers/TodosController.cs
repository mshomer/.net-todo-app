using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.DTO;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly AppDBContext _context;
        private IToDosService _service;

        public TodosController(AppDBContext context, IToDosService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            if (todo.UserId.ToString() != User.Identity.Name)
            {
                return Unauthorized();
            }

            return todo;
        }

        // GET: api/{userId}/todos
        [HttpGet("{userId}/todo")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllUserTodos(int userId)
        {
            var todos = await _service.GetAll(userId);

            if (todos == null)
            {
                return NotFound();
            }

            return Ok(todos);
        }

        // GET: api/{userId}/todos/5
        [HttpGet("{userId}/todo/{id}")]
        public async Task<ActionResult<Todo>> GetUserTodo(int userId, int id)
        {
            var todo = await _service.GetOne(userId, id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/todos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/todos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(CreatedTodoDTO todo)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            var t = await _service.Create(todo);
            return Ok(t);
        }

        // DELETE: api/todos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
