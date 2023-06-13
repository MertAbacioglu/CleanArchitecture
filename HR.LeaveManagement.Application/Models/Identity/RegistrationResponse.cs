namespace HR.LeaveManagement.Application.Models.Identity;

public class RegistrationResponse
{
    public string UserId { get; set; }
    public bool HasError { get; set; }
    public List<string> Errors { get; set; }=new List<string>();
}
