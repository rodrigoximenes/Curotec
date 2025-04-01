using Curotec.Domain.Enums;

namespace Curotec.Application.DTOs
{
    public record TodoRequest
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Assignee { get; init; } = string.Empty;
        public TaskPriorityEnum Priority { get; init; }
    }
}
