using Curotec.Domain.Enums;

namespace Curotec.Domain.Tests
{
    public class TodoTests
    {
        [Theory]
        [InlineData("Title1", "Description1", "John Doe", TaskPriorityEnum.High)]
        [InlineData("Title2", "Description2", "Jane Doe", TaskPriorityEnum.Low)]
        public void Constructor_ShouldInitializeCorrectly(string title, string description, string assignee, TaskPriorityEnum priority)
        {
            var todo = new Todo(title, description, assignee, priority);

            Assert.Equal(title, todo.Title);
            Assert.Equal(description, todo.Description);
            Assert.Equal(assignee, todo.Assignee);
            Assert.Equal(priority, todo.Priority);
            Assert.Equal(TaskStatusEnum.Pending, todo.Status);
            Assert.Null(todo.CompletionDate);
        }

        [Theory]
        [InlineData("New Assignee")]
        public void ChangeAssignee_ShouldUpdateAssignee(string newAssignee)
        {
            var todo = new Todo("Title", "Description", "John Doe", TaskPriorityEnum.Medium);
            todo.ChangeAssignee(newAssignee);

            Assert.Equal(newAssignee, todo.Assignee);
        }

        [Fact]
        public void CompleteTask_ShouldSetStatusToCompleted()
        {
            var todo = new Todo("Title", "Description", "John Doe", TaskPriorityEnum.Medium);
            todo.CompleteTask();

            Assert.Equal(TaskStatusEnum.Completed, todo.Status);
            Assert.NotNull(todo.CompletionDate);
        }

        [Fact]
        public void StartTask_ShouldSetStatusToInProgress()
        {
            var todo = new Todo("Title", "Description", "John Doe", TaskPriorityEnum.Medium);
            todo.StartTask();

            Assert.Equal(TaskStatusEnum.InProgress, todo.Status);
        }

        [Fact]
        public void CancelTask_ShouldSetStatusToCanceled()
        {
            var todo = new Todo("Title", "Description", "John Doe", TaskPriorityEnum.Medium);
            todo.CancelTask();

            Assert.Equal(TaskStatusEnum.Canceled, todo.Status);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameId()
        {
            var todo1 = new Todo("Title", "Description", "John Doe", TaskPriorityEnum.Medium);
            var todo2 = new Todo("Another Title", "Another Description", "Jane Doe", TaskPriorityEnum.High);
            todo2.GetType().GetProperty("Id").SetValue(todo2, todo1.Id);

            Assert.True(todo1.Equals(todo2));
        }
    }
}