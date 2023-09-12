namespace AP.ChevronCoop.Commons;

public class CoreAppSettings
{
    public string ConnectionStrings { get; set; }
    public string WebBaseUrl { get; set; }
    public int MaxRetries { get; set; }

    public string SendGridKey { get; set; }
    public string SendGridSenderEmail { get; set; }
    public string SendGridSenderName { get; set; }
    public bool UseSMTP { get; set; }
    public string SMTPServer { get; set; }
    public int SMTPPort { get; set; }
    public string FromAddress { get; set; }
    public string FromAddressTitle { get; set; }
    public string SMTPUserName { get; set; }
    public string SMTPPassword { get; set; }
    public bool EnableSSL { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public string SuperAdminEmail { get; set; }
    public string SuperAdminPassword { get; set; }
    public string ChevronApiKey { get; set; }
    public string ChevronBaseUrl { get; set; }
    public string ChevronKey { get; set; }
    public string NetPayUATAPIKey { get; set; }
    public string NetPayPRODAPIKey { get; set; }
    public decimal SavingRegularDefaultPayrollAmount { get; set; }
    public decimal SavingRetireDefaultPayrollAmount { get; set; }

}