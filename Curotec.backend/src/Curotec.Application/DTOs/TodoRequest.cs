using Curotec.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Curotec.Application.DTOs
{
    public class TodoRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be at most 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [Range((int)TaskPriorityEnum.Low, (int)TaskPriorityEnum.Critical, ErrorMessage = "Priority is invalid (Low = 0, Medium = 1, High = 2, Critical = 3.)")]
        public TaskPriorityEnum Priority { get; set; }

        [Required(ErrorMessage = "Assignee is required.")]
        public string Assignee { get; set; }
    }
}
