namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class Response<T> 
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public string ValidationError { get; set; }
    public List<string> ValidationErrors { get; set; }
}
