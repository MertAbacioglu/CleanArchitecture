namespace HR.LeaveManagement.Application.Models.Identity;

public class AuthResponse
{
    public string Id { get; set; }
    public string Token { get; set; }=string.Empty;
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool HasError { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

}
