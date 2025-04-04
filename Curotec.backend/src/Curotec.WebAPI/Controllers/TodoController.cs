using Curotec.Application.DTOs;
using Curotec.Application.Services.Interfaces;
using Curotec.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Curotec.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all TODO tasks")]
        public async Task<ActionResult> GetAllTodos()
        {
            var todos = await _todoService.GetAllAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a TODO task by ID")]
        public async Task<ActionResult> GetTodoById(Guid id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            return todo != null ? Ok(todo) : NotFound($"TODO task with ID {id} not found.");
        }

        [HttpGet("status/{status}")]
        [SwaggerOperation(Summary = "Get TODO tasks by status")]
        public async Task<ActionResult> GetTodosByStatus(TaskStatusEnum status)
        {
            var todos = await _todoService.FindByStatusAsync(status);
            return Ok(todos);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new TODO task")]
        public async Task<ActionResult<TodoResponse>> CreateTodo([FromBody] TodoRequest todoRequest)
        {
            var createdTodo = await _todoService.AddAsync(todoRequest);
            return CreatedAtAction(nameof(GetAllTodos), null, createdTodo);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing TODO task")]
        public async Task<ActionResult> UpdateTodo(Guid id, [FromBody] TodoRequest todoRequest)
        {
            var existingTodo = await _todoService.GetByIdAsync(id);
            if (existingTodo == null) return NotFound($"TODO task with ID {id} not found.");

            var updatedTodo = await _todoService.UpdateAsync(id, todoRequest);
            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a TODO task by ID")]
        public async Task<ActionResult> DeleteTodo(Guid id)
        {
            await _todoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
