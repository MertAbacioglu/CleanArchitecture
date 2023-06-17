using HR.LeaveManagement.Application.Models.Email;

namespace HR.LeaveManagement.Application.Contracts.Email;

public interface IEMailService
{
    Task<bool> SendEmail(EmailMessage emailMessage);
}

