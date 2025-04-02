using Curotec.Application.DTOs;
using Curotec.Domain.Enums;

namespace Curotec.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<TodoResponse> GetByIdAsync(Guid id);
        Task<IEnumerable<TodoResponse>> GetAllAsync();
        Task<IEnumerable<TodoResponse>> FindByStatusAsync(TaskStatusEnum status);
        Task<TodoResponse> AddAsync(TodoRequest todoRequest);
        Task UpdateAsync(Guid id, TodoRequest todoRequest);
        Task DeleteAsync(Guid id);
    }
}
