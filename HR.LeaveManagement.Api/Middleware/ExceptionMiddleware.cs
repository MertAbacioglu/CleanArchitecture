using FluentValidation;
using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Wrappers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetail problem = new();

            switch (ex)
            {
                // Handle BadRequestException
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetail
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                    };
                    break;

                // Handle NotFoundException
                case NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetail
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = NotFound.InnerException?.Message,
                    };
                    break;

                // Handle FluentValidation.ValidationException(consider partial class)
                case FluentValidation.ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetail
                    {
                        Title = validationException.Message,
                        Status = (int)statusCode,
                        Type = nameof(FluentValidation.ValidationException),
                        Detail = validationException.InnerException?.Message,
                        Errors = validationException.Errors.ToDictionary(k => k.PropertyName, v => new[] { v.ErrorMessage })
                    };
                    break;

                // Default case for other exceptions
                default:
                    problem = new CustomProblemDetail
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            // Set the response status code and log the problem
            httpContext.Response.StatusCode = (int)statusCode;
            string logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogWarning(logMessage);

            // Handle response based on the exception type
            if (!(ex is FluentValidation.ValidationException))
            {
                // For non-ValidationException, return the problem as JSON
                await httpContext.Response.WriteAsJsonAsync(problem);
            }
            else
            {
                // For ValidationException, convert errors to a list and return as JSON
                List<string> errors = new();

                if (problem.Errors != null)
                {
                    errors = problem.Errors
                        .SelectMany(item => item.Value.Select(value => $"{item.Key}: {value}"))
                        .ToList();
                }
                await httpContext.Response.WriteAsJsonAsync(errors);
            }
        }



    }
}
