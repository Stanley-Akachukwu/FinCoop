using AP.ChevronCoop.AppCore.Services.AccountAutoCreationServices;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.AppCore.Services.AuditServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppCore.Services.MemberprofileServices;
using Hangfire;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        [Obsolete]
        public static IServiceCollection AddChevronCoopBackgroundServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IApprovalInitiationBackgroundService, ApprovalInitiationBackgroundService>();
            services.AddScoped<IConsolidateApprovalsBackgroundService, ConsolidateApprovalsBackgroundService>();
            services.AddScoped<IMemberBulkUploadRetryBackgroundService, MemberBulkUploadRetryBackgroundService>();
            services.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration["ConnectionStrings:chevroncoop"]));
            services.AddHangfireServer();

            return services;
        }
        public static IServiceCollection AddChevronCoopLogServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IManageApprovalService, ManageApprovalService>();
            services.AddScoped<IApprovalDetailFactory, ApprovalDetailFactory>();
            services.AddScoped<IAutoCreateAccountService, AutoCreateAccountService>();
            services.AddScoped<IFixedDepositInterestComputationService, FixedDepositInterestComputationService>();
            services.AddScoped<ISDDailyInterestComputationService, SDDailyInterestComputationService>();
            services.AddScoped<ISDMonthlyInterestComputationService, SDMonthlyInterestComputationService>();


            return services;
        }
        public static IServiceCollection AddAutoCreateMemberByBulkUploadServices(this IServiceCollection services)
        {
            services.AddScoped<IMemberProfileService, MemberProfileService>();
            services.AddScoped<IAutoCreateAccountService, AutoCreateAccountService>();

            return services;
        }

        public static IServiceCollection AddPayrollScheduleServices(this IServiceCollection services)
        {
            services.AddScoped<IPayrollScheduleBackgroundService, PayrollScheduleBackgroundService>();
            return services;
        }
    }
}
