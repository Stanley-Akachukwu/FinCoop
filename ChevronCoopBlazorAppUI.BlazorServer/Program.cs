using AP.ChevronCoop.AppCore.MasterData.Banks;
using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Areas.Identity;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Refit;
using Simple.OData.Client;
using Syncfusion.Blazor;
using System.Text.Json.Serialization;

namespace ChevronCoop.Web.AppUI.BlazorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Register Syncfusion license
            //20.04
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTA5NzUwMUAzMjMwMmUzNDJlMzBEeVRQVkhwa0VNRnluRmV4SWdhSnFzd3FmUW5KT2ROcXFhbmROcVZEcXVRPQ==");

            //22.1  **has issues with locale/culture do not enable for now
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjU4MzcyOUAzMjMyMmUzMDJlMzBqeTUzOVFYR2FzWTBmU0NlOEtreFZUVkZBa25GVmFKeHJhdm5Vc2RFcDF3PQ==");


            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            builder.Services.AddAntDesign();
            builder.Services.AddDbContext<ChevronCoopDbContext>(options =>
                   options.UseSqlServer(
                       builder.Configuration.GetConnectionString("chevroncoop")));
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ChevronCoopDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            builder.Services.AddScoped(typeof(IUtilityService), typeof(UtilityService));

            builder.Services.AllCustomServices();

            builder.Services.AddSweetAlert2();
            builder.Services.AddScoped<ApprovalStateContainerService>();
            builder.Services.AddScoped<AuthenticationHeaderHandler>();

            // var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ') ?? builder.Configuration["MicrosoftGraph:Scopes"]?.Split(' ');

            // Add services to the container.
            //builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"))
            //        .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            //.AddDownstreamWebApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
            //            .AddInMemoryTokenCaches();
            //builder.Services.AddControllersWithViews()
            //    .AddMicrosoftIdentityUI();

            //builder.Services.AddAuthorization(options =>
            //{
            //    // By default, all incoming requests will be authorized according to the default policy
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});

            // // setup cookies provider


            int sessionTimeOut = int.Parse(builder.Configuration["SESSION_TIMEOUT_MINS"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                    .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme, config =>
                    {
                        config.Cookie.Name = "chevroncoop.cookie";
                        config.Cookie.IsEssential = true;
                        //config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        config.Cookie.SameSite = SameSiteMode.Lax;
                        //config.LoginPath = "/Login";
                        config.LoginPath = new PathString("/identity/account/Login");

                        config.SlidingExpiration = true;
                        config.Cookie.HttpOnly = true;
                        //config.ExpireTimeSpan = TimeSpan.FromMinutes(sessionTimeOut);
                        config.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;


                    });


            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.ConsentCookie.IsEssential = true;
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;

            });

            //end


            // Add services to the container.
            builder.Services.AddRazorPages().AddJsonOptions(options =>
            {

                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
                //options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());

            });

            builder.Services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
            //builder.Services.AddControllers();

            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddAntDesign();
            builder.Services.AddBlazoredSessionStorage();
            var settings = new RefitSettings();
            builder.Services.AddRefitClient<IEntityDataService>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConfigKeys.API_HOST]))
                    .AddHttpMessageHandler<AuthenticationHeaderHandler>();

            builder.Services.AddScoped<HttpClient>(s =>
            {
                return new HttpClient { BaseAddress = new Uri(builder.Configuration[ConfigKeys.API_HOST]) };
            });


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<HttpContextAccessor>();

            builder.Services.AddScoped<WebConfigHelper>();
            builder.Services.AddScoped<ODataClient>(options => { return new ODataClient(builder.Configuration["ENTTIY_ODATA_HOST"]); });
            builder.Services.AddScoped<FileDownloader>();
            builder.Services.AddScoped<BrowserService>();
            builder.Services.AddScoped<IUserDownLoadService, UserDownLoadService>();
            builder.Services.AddScoped<IFileExportService, FileExportService>();
            builder.Services.AddScoped<IApprovalDetailFactory, ApprovalDetailFactory>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            //builder.Services.AddScoped<IEntityDataService>();

            builder.Services.AddAutoMapper(typeof(BankMapperProfile).Assembly);

            //builder.Services.AddValidatorsFromAssembly(typeof(CreateBankCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssemblyContaining<CreateBankCommandValidator>();

            // services.AddProtectedBrowserStorage();
            //services.AddBlazoredLocalStorage();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapBlazorHub();
            //app.MapFallbackToPage("/_Host");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });

            app.Run();
        }
    }
}