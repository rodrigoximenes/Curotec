using AutoMapper;
using Curotec.Application.DTOs;
using Curotec.Application.Services;
using Curotec.Application.Services.Interfaces;
using Curotec.Data.Repository.Interfaces;
using Curotec.Domain;
using Curotec.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Curotec.Application.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<IRepository<Todo>> _todoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<Todo>> _todoValidatorMock;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _todoRepositoryMock = new Mock<IRepository<Todo>>();
            _mapperMock = new Mock<IMapper>();
            _todoValidatorMock = new Mock<IValidator<Todo>>();

            _todoService = new TodoService(
                _todoRepositoryMock.Object,
                _mapperMock.Object,
                _todoValidatorMock.Object
            );
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsException_WhenTodoDoesNotExist()
        {
            var todoId = Guid.NewGuid();
            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId)).ReturnsAsync((Todo)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoService.GetByIdAsync(todoId));
        }

        [Fact]
        public async Task AddAsync_ThrowsValidationException_WhenValidationFails()
        {
            var todoRequest = new TodoRequest { Title = "", Description = "" };

            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Title", "Title is required"),
                new ValidationFailure("Description", "Description is required")
            });

            var todo = new Todo(todoRequest.Title, todoRequest.Description, "Assignee", TaskPriorityEnum.Low);

            _mapperMock.Setup(m => m.Map<Todo>(todoRequest)).Returns(todo);
            _todoValidatorMock.Setup(v => v.ValidateAsync(todo, default)).ReturnsAsync(validationResult);

            await Assert.ThrowsAsync<ValidationException>(() => _todoService.AddAsync(todoRequest));
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTodoSuccessfully()
        {
            var todoId = Guid.NewGuid();
            var todoRequest = new TodoRequest
            {
                Title = "Updated Task",
                Description = "Updated Description",
                Assignee = "Assignee",
                Priority = TaskPriorityEnum.High
            };

            var existingTodo = new Todo("Existing Task", "Existing Description", "Assignee", TaskPriorityEnum.Medium)
            {
                Id = todoId
            };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId)).ReturnsAsync(existingTodo);
            _mapperMock.Setup(m => m.Map(todoRequest, existingTodo));
            _todoValidatorMock.Setup(v => v.ValidateAsync(existingTodo, default)).ReturnsAsync(new ValidationResult());

            await _todoService.UpdateAsync(todoId, todoRequest);

            _todoRepositoryMock.Verify(repo => repo.UpdateAsync(existingTodo), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesTodoSuccessfully()
        {
            var todoId = Guid.NewGuid();
            var existingTodo = new Todo("Existing Task", "Existing Description", "Assignee", TaskPriorityEnum.Medium)
            {
                Id = todoId
            };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId)).ReturnsAsync(existingTodo);

            await _todoService.DeleteAsync(todoId);

            _todoRepositoryMock.Verify(repo => repo.DeleteAsync(todoId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ThrowsException_WhenTodoNotFound()
        {
            var todoId = Guid.NewGuid();
            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId)).ReturnsAsync((Todo)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoService.DeleteAsync(todoId));
        }
    }
}
