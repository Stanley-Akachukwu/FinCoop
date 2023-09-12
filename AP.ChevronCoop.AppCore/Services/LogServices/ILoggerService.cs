namespace AP.ChevronCoop.AppCore.Services.LogServices
{
    public interface ILoggerService
    {
        Task LogInfo(string className, string methodName, string message);
        Task LogError(string className, string methodName, Exception exception);
    }
}
