using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using SendGrid;
using SendGrid.Helpers.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email;

public class EmailService : IEmailService
{
    private readonly CoreAppSettings _emailConfig;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<CoreAppSettings> emailConfig, ILogger<EmailService> logger)
    {
        _emailConfig = emailConfig.Value;
        _logger = logger;
    }


    public void SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);

        Send(emailMessage);
    }

    public async Task SendEmailAsync(Message message)
    {
        var mailMessage = CreateEmailMessage(message);
        await SendAsync(mailMessage);
    }

    public async Task SendEmailAsync(EmailTemplateType type, string email, dynamic props)
    {
        string msg = String.Empty;
        string subject = string.Empty;
        switch (type)
        {
            case EmailTemplateType.ENROLLMENT:
                {
                    var dto = (GeneralEmailDto)props;
                    subject = "CEMCS Enrollment";
                    msg = EmailTemplates.CreateEnrollmentEmail(dto.Name, dto.Link);
                    break;
                }
            case EmailTemplateType.GUARANTOR_APPROVAL:
                {
                    var dto = (GeneralEmailDto)props;
                    subject = "Guarantor Request";
                    msg = EmailTemplates.CreateGuarantorApproval(dto.Name, dto.Link);
                    break;
                }
            case EmailTemplateType.APPROVAL_NOTIFICATION:
                {
                    var dto = (GeneralEmailDto)props;
                    subject = "Approval Request";
                    msg = EmailTemplates.CreateApprovalNotification(dto.Name, dto.Link);
                    break;
                }
            case EmailTemplateType.APPROVAL_REJECTION_NOTIFICATION:
                {
                    var dto = (GeneralEmailDto)props;
                    subject = "Approval Request";
                    msg = EmailTemplates.CreateApprovalRejectionNotification(dto.Name, dto.Link, dto.Description);
                    break;
                }
            case EmailTemplateType.LOAN_APPROVAL:
                {
                    var dto = (GeneralEmailDto)props;
                    subject = "CEMCS Loan Approval";
                    msg = EmailTemplates.CreateLoanApproval(dto.Name, dto.Link);
                    break;
                }
            case EmailTemplateType.FORGOT_PASSWORD:
                {
                    var dto = (ForgotPasswordEmailDto)props;
                    subject = "Reset Password";
                    msg = EmailTemplates.CreateForgotPasswordEmail(dto.OTP, dto.Name);
                    break;
                }
            case EmailTemplateType.CREATE_APPLICATION_USER:
                {
                    var dto = (CreateApplicationUserEmailDto)props;
                    subject = "CEMCS Account";
                    msg = EmailTemplates.CreateApplicationUserEmail(dto.Name, dto.Link, dto.Password);
                    break;
                }
            case EmailTemplateType.LOAN_PRODUCT_INITIATION:
                {
                    var dto = (CreateLoanProductAlertEmailDto)props;
                    subject = "CEMCS Account";
                    msg = EmailTemplates.CreateLoanProductAlert(dto.Name, dto.Link);
                    break;
                }
            case EmailTemplateType.DEPOSIT_APPLICATION_APPROVAL:
                {
                    var dto = (DepositApplicationApproval)props;
                    subject = "CEMCS Deposit Application Approval";
                    msg = EmailTemplates.CreateDepositApplicationApprovalAlert(dto.Name, dto.DepositName);
                    break;
                }
            case EmailTemplateType.DEPOSIT_ACTION:
                {
                    var dto = (DepositAction)props;
                    subject = "CEMCS Deposit Action";
                    msg = EmailTemplates.CreateDepositActionApprovalAlert(dto.Name, dto.ActionMessage);
                    break;
                }
            case EmailTemplateType.DEPOSITWORKFLOW_APPROVAL:
                {
                    var dto = (CreateDepositProductAlertEmailDto)props;
                    subject = "CEMCS Account/Action Approval";
                    msg = EmailTemplates.CreateDepositWorkflowApprovalAlert(dto.Name, dto.Link);
                    break;
                }
            case EmailTemplateType.PENDING_PAYROLL_PAYMENT:
                {
                    var dto = (GeneralEmailDto)props;
                    subject = "CEMCS Pending payroll payment";
                    msg = EmailTemplates.CreatePendingPayrollPayment(dto.Name, dto.Link);
                    break;
                }
        }

        // if (type == EmailTemplateType.PRIVILEDGE_APPROVAL)
        // {
        //     var dto = (ApprovalEmailDto)props;
        //     subject = "CEMCS Enrollment";
        //     msg = EmailTemplates.CreateApprovalEmail(dto.Name, dto.Link);
        // }

        if (_emailConfig.UseSMTP)
        {
            var message = new Message(email, subject, msg);
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }
        else
        {
            // await SendAsync(subject, email, msg);

            await SendCEMCAsync(new EmailRequest
            {
                subject = subject,
                html = msg,
                to = email
            });
        }
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_emailConfig.SMTPServer, _emailConfig.SMTPPort, SecureSocketOptions.StartTls);
                //client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.SMTPUserName, _emailConfig.SMTPPassword);
                await client.SendAsync(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception, or both.
                //throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }

    private async Task SendAsync(string subject, string to, string message)
    {
        try
        {
            var apiKey = _emailConfig.SendGridKey;
            var client = new SendGridClient(apiKey);
            var sender = new EmailAddress(_emailConfig.SendGridSenderEmail, _emailConfig.SendGridSenderName);
            var receiver = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(sender, receiver, subject, null, message);
            var response = await client.SendEmailAsync(msg);
        }
        catch
        {

        }
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.FromAddressTitle, _emailConfig.FromAddress));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Body };

        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SMTPServer, _emailConfig.SMTPPort, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.SMTPUserName, _emailConfig.SMTPPassword);

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }

    public async Task SendCEMCAsync(EmailRequest emailRequest)
    {
        string url = $"{_emailConfig.ChevronBaseUrl}/mail/send";

        try
        {

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? uri))
            {
                string json = emailRequest.ToJsonString();

                Dictionary<string, string> headers = new()
                    {
                        //{ "Accept", "application/json" },
                        {"APIKEY", _emailConfig.ChevronApiKey }
                    };

                HttpClientHandler clientHandler = new()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                using var httpClient = new HttpClient(clientHandler);

                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                // FormUrlEncodedContent
                var httpContent = new StringContent(json, Encoding.UTF8,
                                "application/json");

                var response = await httpClient.PostAsync(uri, httpContent);

                string message = await response.Content.ReadAsStringAsync();

                _logger.LogInformation(message ?? "");
            }
        }
        catch (Exception ex)
        {
            Debug.Write(ex);
            _logger.LogInformation(ex?.Message ?? "");
        }
        // _logger.LogInformation($"Email response: {response.Content.ToJsonString()} Http Status: {response.StatusCode}");
    }

}


