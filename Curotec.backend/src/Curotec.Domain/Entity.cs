using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curotec.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
        
        public bool IsValid => ValidationResult?.IsValid ?? false;
    }
}
