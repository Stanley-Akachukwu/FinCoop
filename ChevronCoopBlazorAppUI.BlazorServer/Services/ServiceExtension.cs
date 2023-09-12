using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Users;
using ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService;
using ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Services.LoanProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Services.LoanProducts.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
    public static class ServiceExtension
    {
        public static void AllCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IClientAuditService, ClientAuditService>();
            services.AddSingleton<TempObjectService>();
            services.AddScoped<CustomerHelper>();
            services.AddScoped<UserService>();
            services.AddScoped<ICustomersMasterViews, CustomersMasterView>();
            services.AddScoped<IAggregationService, AggregationService>();
            services.AddScoped<IMasterViews, MasterViews>();
            services.AddScoped<ILoanHelperService, LoanHelperService>();
        }
    }
}
