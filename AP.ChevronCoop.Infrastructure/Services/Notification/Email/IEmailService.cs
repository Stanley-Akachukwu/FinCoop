using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;

namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email;

public interface IEmailService
{
    void SendEmail(Message message);
    Task SendEmailAsync(Message message);
    Task SendEmailAsync(EmailTemplateType type, string email, dynamic props);

    Task SendCEMCAsync(EmailRequest emailRequest);
}