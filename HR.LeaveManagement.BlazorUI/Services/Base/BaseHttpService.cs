using Blazored.LocalStorage;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected readonly IClient _client;
    protected readonly ILocalStorageService _localStorageService;

    public BaseHttpService(IClient client, ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
    }
    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        Console.WriteLine("ConvertApiExceptions method called");

        switch (ex.StatusCode)
        {
            case 400:
                Console.WriteLine("400 error: Invalid data was submitted");
                return new Response<Guid>() { Message = "Invalid data was submitted", ValidationError = ex.Response, Success = false };
            case 401:
                Console.WriteLine("401 error: Unauthorized access");
                return new Response<Guid>() { Message = "Unauthorized access", Success = false };
            case 403:
                Console.WriteLine("403 error: Forbidden access");
                return new Response<Guid>() { Message = "Forbidden access", Success = false };
            case 404:
                Console.WriteLine("404 error: The record was not found");
                return new Response<Guid>() { Message = "The record was not found", Success = false };
            case 409:
                Console.WriteLine("409 error: Conflict occurred");
                return new Response<Guid>() { Message = "Conflict occurred", Success = false };
            case 500:
                Console.WriteLine("500 error: Internal server error");
                return new Response<Guid>() { Message = "Internal server error", Success = false };
            default:
                Console.WriteLine("Unknown error occurred");
                return new Response<Guid>() { Message = "Something went wrong, please try again later.", Success = false };
        }
    }


}