namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;

public class CreateApplicationUserEmailDto
{
  public string Name { get; set; }
  public string Link { get; set; }
  public string Password { get; set; }
}

public class CreateLoanProductAlertEmailDto
{
  public string Name { get; set; }
  public string Link { get; set; }
}