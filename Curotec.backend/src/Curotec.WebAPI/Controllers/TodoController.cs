using Curotec.Application.DTOs;
using Curotec.Application.Services.Interfaces;
using Curotec.Domain;
using Curotec.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Curotec.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing TODO tasks.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Get all TODO tasks.
        /// </summary>
        /// <returns>A list of TODO tasks.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all TODO tasks", Description = "Returns a list of all TODO tasks.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            try
            {
                var todos = await _todoService.GetAllAsync();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a TODO task by ID.
        /// </summary>
        /// <param name="id">The ID of the TODO task.</param>
        /// <returns>A TODO task.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a TODO task by ID", Description = "Returns a TODO task by its unique ID.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            try
            {
                var todo = await _todoService.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound($"TODO task with ID {id} not found.");
                }
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all TODO tasks by status.
        /// </summary>
        /// <param name="status">The status of the TODO tasks.</param>
        /// <returns>A list of TODO tasks by status.</returns>
        [HttpGet("status/{status}")]
        [SwaggerOperation(Summary = "Get TODO tasks by status", Description = "Returns a list of TODO tasks filtered by status.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosByStatus(TaskStatusEnum status)
        {
            try
            {
                var todos = await _todoService.FindByStatusAsync(status);
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Create a new TODO task.
        /// </summary>
        /// <param name="todoCreateRequest">The TODO task to create.</param>
        /// <returns>The created TODO task.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new TODO task", Description = "Creates a new TODO task.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Todo>> CreateTodo([FromBody] TodoRequest todoCreateRequest)
        {
            try
            {
                var todo = new Todo(todoCreateRequest.Title, todoCreateRequest.Description, todoCreateRequest.Assignee, todoCreateRequest.Priority);
                await _todoService.AddAsync(todo);
                return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing TODO task.
        /// </summary>
        /// <param name="id">The ID of the TODO task to update.</param>
        /// <param name="todoUpdateRequest">The updated TODO task data.</param>
        /// <returns>No content (204) on success.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing TODO task", Description = "Updates an existing TODO task.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] TodoRequest todoUpdateRequest)
        {
            try
            {
                var todo = await _todoService.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound($"TODO task with ID {id} not found.");
                }

                todo.ChangeAssignee(todoUpdateRequest.Assignee); // Example of an update action
                await _todoService.UpdateAsync(todo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a TODO task by ID.
        /// </summary>
        /// <param name="id">The ID of the TODO task to delete.</param>
        /// <returns>No content (204) on success.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a TODO task by ID", Description = "Deletes a TODO task by its unique ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            try
            {
                var todo = await _todoService.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound($"TODO task with ID {id} not found.");
                }

                await _todoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
