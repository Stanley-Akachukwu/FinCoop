
namespace AP.ChevronCoop.AppCore.Services.AuditServices
{
    public interface IAuditService
    {
        Task<bool> LogAudit(string action, string description, string module, string createdByUserId, string payload, string username);
    }
}





