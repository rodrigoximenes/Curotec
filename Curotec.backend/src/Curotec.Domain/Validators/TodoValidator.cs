using FluentValidation;

namespace Curotec.Domain.Validators
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(todo => todo.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(todo => todo.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(todo => todo.Assignee)
                .NotEmpty().WithMessage("Assignee cannot be empty.");

            RuleFor(todo => todo.Priority)
                .IsInEnum().WithMessage("Invalid priority value.");
        }
    }
}
