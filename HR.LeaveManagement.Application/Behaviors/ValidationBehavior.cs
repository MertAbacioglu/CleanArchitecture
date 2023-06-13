using FluentValidation.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    //public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //{
    //    if (!_validators.Any())
    //    {
    //        return await next();
    //    }

    //    ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
    //    Dictionary<string, string> errorsDictionary = _validators
    //        .Select(v => v.Validate(context))
    //        .SelectMany(result => result.Errors)
    //        .Where(f => f != null)
    //        .GroupBy(
    //        s => s.PropertyName,
    //        s => s.ErrorMessage, (key, errors) => new
    //        {
    //            Key = key,
    //            Values = errors.Distinct().ToArray()
    //        }
    //        ).ToDictionary(s => s.Key, s => s.Values[0]);

    //    if (errorsDictionary.Any())
    //    {
    //        IEnumerable<ValidationFailure> errors = errorsDictionary.Select(s => new ValidationFailure
    //        {
    //            PropertyName = s.Value,
    //            ErrorCode = s.Key,
    //        });
    //        throw new ValidationException(errors);
    //    }
    //    return await next();
    //}

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        Dictionary<string, string> errorsDictionary = new Dictionary<string, string>();

        foreach (var validator in _validators)
        {
            ValidationResult validationResult = await validator.ValidateAsync(context);
            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure? error in validationResult.Errors)
                {
                    errorsDictionary[error.PropertyName] = error.ErrorMessage;
                }
            }
        }

        if (errorsDictionary.Any())
        {
            IEnumerable<ValidationFailure> errors = errorsDictionary.Select(s => new ValidationFailure
            {
                PropertyName = s.Value,
                ErrorCode = s.Key,
            });
            throw new ValidationException(errors);
        }

        return await next();
    }
}
