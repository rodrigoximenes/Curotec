using Curotec.Domain.Enums;

namespace Curotec.Domain
{
    public class Todo : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatusEnum Status { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? CompletionDate { get; private set; }
        public string Assignee { get; private set; }
        public TaskPriorityEnum Priority { get; private set; }

        public Todo(string title, string description, string assignee, TaskPriorityEnum priority)
        {
            Title = title;
            Description = description;
            Status = TaskStatusEnum.Pending;
            CreationDate = DateTime.Now;
            Assignee = assignee;
            Priority = priority;
        }
        public void ChangeAssignee(string newAssignee)
        {
            Assignee = newAssignee;
        }

        public void CompleteTask()
        {
            Status = TaskStatusEnum.Completed;
            CompletionDate = DateTime.Now;
        }

        public void StartTask()
        {
            Status = TaskStatusEnum.InProgress;
        }

        public void CancelTask()
        {
            Status = TaskStatusEnum.Canceled;
        }
    }
}
