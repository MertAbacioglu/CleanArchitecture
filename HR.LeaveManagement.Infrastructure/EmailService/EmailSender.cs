using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace HR.LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    public EmailSetting _emailSetting { get; }

    public EmailSender(IOptions<EmailSetting> emailSetting)
    {
        _emailSetting = emailSetting.Value;
    }

    public async Task<bool> SendEmail(EmailMessage email)
    {
        SendGridClient client = new SendGridClient(_emailSetting.ApiKey);
        EmailAddress to = new EmailAddress(email.To);
        EmailAddress from = new EmailAddress
        {
            Email = _emailSetting.FromAddress,
            Name = _emailSetting.FromName
        };
        SendGridMessage message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        Response response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
