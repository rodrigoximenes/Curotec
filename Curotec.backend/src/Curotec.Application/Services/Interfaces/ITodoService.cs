using Curotec.Domain;
using Curotec.Domain.Enums;

namespace Curotec.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<Todo> GetByIdAsync(int id);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<IEnumerable<Todo>> FindByStatusAsync(TaskStatusEnum status);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(int id);
    }
}
