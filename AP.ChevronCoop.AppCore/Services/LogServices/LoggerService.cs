using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Services.LogServices
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public async Task LogError(string className, string methodName, Exception exception)
        {
            var task = Task.Run(() => _logger.LogError(exception, $"\r\nExecuting Operation: {className} Method Name: {methodName}\r\nMessage: {2}\r\n"));
        }

        public async Task LogInfo(string className, string methodName, string message)
        {
            var task = Task.Run(() => _logger.LogInformation("\r\nExecuting Operation: {1} Method Name: {2}\r\nMessage: {3}\r\n", className, methodName, message));
        }

    }

}
