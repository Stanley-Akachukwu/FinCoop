using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using System.Net;
using System.Net.Sockets;

namespace AP.ChevronCoop.AppCore.Services.AuditServices
{
    public class AuditService : IAuditService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public AuditService(ChevronCoopDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        public async Task<bool> LogAudit(string action, string description, string module, string createdByUserId, string payload, string username)
        {
            var ipAddress = GetLocalIPAddress();
            try
            {
                var entityId = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString();
                var auditLog = new AuditTrail
                {
                    Action = action,
                    Description = description,
                    Module = module,
                    CreatedByUserId = createdByUserId,
                    Payload = payload,
                    ApplicationUserId = createdByUserId,
                    IPAddress = ipAddress,
                    DateCreated = DateTime.UtcNow,
                    UserName = username,
                    Id = entityId,
                    Timestamp = DateTime.UtcNow,
                };

                await _dbContext.AuditTrails.AddAsync(auditLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _loggerService.LogError(nameof(AuditService), nameof(LogAudit), ex);
            }
            return await Task.FromResult(true);
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "NA";
        }

    }
}
