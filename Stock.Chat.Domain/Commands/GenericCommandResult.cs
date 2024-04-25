using FluentValidation.Results;
using Stock.Chat.CrossCutting.Commands;

namespace Stock.Chat.Domain.Commands
{
    public abstract class GenericCommandResult<T> : ICommandResult<T>
    {
        protected GenericCommandResult() => ValidationResult = new ValidationResult();
        protected ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
        public virtual IList<ValidationFailure> GetErrors() => ValidationResult.Errors;
    }
}
