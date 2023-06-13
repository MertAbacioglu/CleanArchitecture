using FluentValidation;
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public partial class ValidationException
{
    public ValidationException(string message, ValidationResult validationResult)
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    public IDictionary<string, string[]> ValidationErrors { get; set; }
}
