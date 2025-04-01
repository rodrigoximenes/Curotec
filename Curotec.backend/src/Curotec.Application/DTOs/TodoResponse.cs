using Curotec.Domain.Enums;

namespace Curotec.Application.DTOs
{
    public record TodoResponse
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public TaskStatusEnum Status { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime? CompletionDate { get; init; }
        public string Assignee { get; init; }
        public TaskPriorityEnum Priority { get; init; }
    }
}
