namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;

public class GeneralEmailDto
{
  public string Name { get; set; }
  public string Link { get; set; }
  public string Description { get; set; }
}

public class ForgotPasswordEmailDto
{
  public string OTP { get; set; }
  public string Name { get; set; }  
}