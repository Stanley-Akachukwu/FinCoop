using MimeKit;

namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email;

public class Message
{
  public List<MailboxAddress> To { get; set; }
  public string Subject { get; set; }
  public string Body { get; set; }

  public Message(List<string> to, string subject, string body)
  {
    To = new List<MailboxAddress>();

    To.AddRange(to.Select(x => 
      new MailboxAddress("", x)));
    Subject = subject;
    Body = body;
  }
  
  public Message(string to, string subject, string body)
  {
    To = new List<MailboxAddress> { new MailboxAddress("", to) };
    Subject = subject;
    Body = body;
  }
}