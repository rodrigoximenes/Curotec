using Curotec.Application.Services.Interfaces;
using Curotec.Data.Repository.Interfaces;
using Curotec.Domain;
using Curotec.Domain.Enums;

namespace Curotec.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<Todo> _todoRepository;

        public TodoService(IRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Todo>> FindByStatusAsync(TaskStatusEnum status)
        {
            return await _todoRepository.FindAsync(todo => todo.Status == status);
        }

        public async Task AddAsync(Todo todo)
        {
            await _todoRepository.AddAsync(todo);
        }

        public async Task UpdateAsync(Todo todo)
        {
            await _todoRepository.UpdateAsync(todo);
        }

        public async Task DeleteAsync(int id)
        {
            await _todoRepository.DeleteAsync(id);
        }
    }
}