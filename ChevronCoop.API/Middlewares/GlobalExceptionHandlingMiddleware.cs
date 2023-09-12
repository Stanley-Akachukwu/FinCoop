using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Commons;
using System.Net;
using System.Text.Json;

namespace ChevronCoop.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILoggerService _logger;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> appLogger;
        ILoggerFactory loggerFactory;
        public GlobalExceptionHandlingMiddleware(ILoggerService logger, ILoggerFactory _loggerFactory)
        {
            _logger = logger;
            loggerFactory = _loggerFactory;
            appLogger = loggerFactory.CreateLogger<GlobalExceptionHandlingMiddleware>();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); 
            }
            catch (Exception ex)
            {
                appLogger.LogError(ex, ex.Message);

                await _logger.LogError(nameof(GlobalExceptionHandlingMiddleware), nameof(InvokeAsync), ex);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var rsp = new CommandResult<string>
                {
                    ErrorFlag = true,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"Internal server error has occurred.{ex.Message} \n {ex.InnerException?.Message}",
                    Detail = ex.StackTrace
                };

                string jsonRsp = JsonSerializer.Serialize(rsp);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonRsp);
            }
        }
    }
}
