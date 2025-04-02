using AutoMapper;
using Curotec.Application.DTOs;
using Curotec.Application.Services.Interfaces;
using Curotec.Data.Repository.Interfaces;
using Curotec.Domain;
using Curotec.Domain.Enums;
using FluentValidation;

namespace Curotec.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<Todo> _todoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Todo> _todoValidator;

        public TodoService(IRepository<Todo> todoRepository, IMapper mapper, IValidator<Todo> todoValidator)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _todoValidator = todoValidator;
        }

        public async Task<TodoResponse> GetByIdAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            return todo == null ? throw new KeyNotFoundException("TODO task not found") : _mapper.Map<TodoResponse>(todo);
        }

        public async Task<IEnumerable<TodoResponse>> GetAllAsync()
        {
            var todos = await _todoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TodoResponse>>(todos);
        }

        public async Task<IEnumerable<TodoResponse>> FindByStatusAsync(TaskStatusEnum status)
        {
            var todos = await _todoRepository.FindAsync(todo => todo.Status == status);
            return _mapper.Map<IEnumerable<TodoResponse>>(todos);
        }

        public async Task<TodoResponse> AddAsync(TodoRequest todoRequest)
        {
            var todo = _mapper.Map<Todo>(todoRequest);

            var validationResult = await _todoValidator.ValidateAsync(todo);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            await _todoRepository.AddAsync(todo);
            return _mapper.Map<TodoResponse>(todo);
        }

        public async Task UpdateAsync(Guid id, TodoRequest todoRequest)
        {
            var existingTodo = await _todoRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("TODO task not found");

            _mapper.Map(todoRequest, existingTodo);
            var validationResult = await _todoValidator.ValidateAsync(existingTodo);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            await _todoRepository.UpdateAsync(existingTodo);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingTodo = await _todoRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("TODO task not found");

            await _todoRepository.DeleteAsync(id);
        }
    }
}
