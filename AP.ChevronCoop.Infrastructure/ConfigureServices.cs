using AP.ChevronCoop.AppCore.ChevronAPIs;
using AP.ChevronCoop.AppCore.ChevronAPIs.Interfaces;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.Extensions.DependencyInjection;

namespace AP.ChevronCoop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IChevronNetPayService, ChevronNetPayService>();
        return services;
    }
}