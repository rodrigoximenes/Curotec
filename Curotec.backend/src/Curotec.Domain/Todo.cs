using System;
using Curotec.Domain.Enums;
using Curotec.Domain.Validators;
using System.Collections.Generic;

namespace Curotec.Domain
{
    public class Todo : Entity, IEquatable<Todo>
    {
        private static readonly TodoValidator _validator = new TodoValidator();

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

            ValidationResult = _validator.Validate(this);
        }

        public void ChangeAssignee(string newAssignee)
        {
            Assignee = newAssignee;
            ValidationResult = _validator.Validate(this);
        }

        public void CompleteTask()
        {
            Status = TaskStatusEnum.Completed;
            CompletionDate = DateTime.Now;
            ValidationResult = _validator.Validate(this);
        }

        public void StartTask()
        {
            Status = TaskStatusEnum.InProgress;
            ValidationResult = _validator.Validate(this);
        }

        public void CancelTask()
        {
            Status = TaskStatusEnum.Canceled;
            ValidationResult = _validator.Validate(this);
        }

        public bool Equals(Todo other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Todo);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
