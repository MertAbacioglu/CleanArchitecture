using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HR.LeaveManagement.Infrastructure.EmailService;
using MailKit.Net.Smtp;

public class EMailService : IEMailService
{
    public EmailSetting _emailSetting { get; }

    public EMailService(IOptions<EmailSetting> emailSetting)
    {
        _emailSetting = emailSetting.Value;
    }

    public async Task<bool> SendEmail(EmailMessage emailMessage)
    {
        MimeMessage email = new();
        email.Sender = MailboxAddress.Parse($"{_emailSetting.DisplayName} < {_emailSetting.EmailFrom}>");
        email.To.Add(MailboxAddress.Parse(emailMessage.To));
        email.Subject = emailMessage.Subject;
        BodyBuilder builder = new();
        builder.HtmlBody = emailMessage.Body;
        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new();
        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
        smtp.Connect(_emailSetting.SmtpHost, _emailSetting.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSetting.SmtpUser, _emailSetting.SmtpPass);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);

        return true;

        //todo:check if email is sent or not and return true or false
    }
}