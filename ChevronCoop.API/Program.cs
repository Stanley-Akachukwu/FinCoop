using System.Text;
using System.Text.Json.Serialization;
using AP.ChevronCoop.AppCore.Loans.LoanProducts;
using AP.ChevronCoop.AppCore.MasterData.Banks;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.Seeder;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Infrastructure;
using Audit.Core;
using ChevronCoop.API.Config;
using ChevronCoop.API.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ChevronCoop.API;

public class Program
{
    [Obsolete]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string env = builder.Environment.EnvironmentName;
       // env = "Staging";
        builder.Configuration
          .AddJsonFile("appsettings.json", false, true)
          .AddJsonFile($"appsettings.{env}.json", true)
          .AddEnvironmentVariables();


        //var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
        //  .CreateLogger();
        //builder.Logging.ClearProviders();
        //builder.Logging.AddSerilog(logger);


        //builder.Logging.ClearProviders();

        // TODO: Uncomment b4 prod to clear console loggers
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration),
        writeToProviders: true);



        builder.Services.AddDbContext<ChevronCoopDbContext>(options =>
          options.UseSqlServer(
            builder.Configuration.GetConnectionString("chevroncoop")));
        //services.AddMeMoryCache();

        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
        builder.Services.AddScoped<ValidationFilterAttribute>();
        builder.Services.AddMemoryCache();

        builder.Services.AddChevronCoopBackgroundServices(builder.Configuration)
          .AddChevronCoopLogServices()
          .AddAutoCreateMemberByBulkUploadServices();

        // JWT authentication handler
        builder.Services.AddAuthentication(config =>
          {
              config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
              config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer("OAuth", config =>
          {
              var secretBytes = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty);
              var key = new SymmetricSecurityKey(secretBytes);

              config.TokenValidationParameters = new TokenValidationParameters
              {
                  IssuerSigningKey = key,
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = builder.Configuration["Jwt:Issuer"],
                  ValidAudience = builder.Configuration["Jwt:Issuer"],
                  ClockSkew = TimeSpan.Zero
              };
          });

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
          {
              config.Password.RequiredLength = 8;
              config.Password.RequireDigit = false;
              config.Password.RequireNonAlphanumeric = false;
              config.Password.RequireUppercase = false;
              config.SignIn.RequireConfirmedEmail = false;
          })
          .AddEntityFrameworkStores<ChevronCoopDbContext>()
          .AddDefaultTokenProviders();

        builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
          opt.TokenLifespan = TimeSpan.FromHours(1));

        // End JWT authentication handler
        var maxRequestLimit = int.Parse(builder.Configuration["MaxRequestBodySize"]);
        // If using IIS
        builder.Services.Configure<IISServerOptions>(options => { options.MaxRequestBodySize = maxRequestLimit; });
        // If using Kestrel
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = maxRequestLimit;
        });
        builder.Services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = maxRequestLimit;
            x.MultipartBodyLengthLimit = maxRequestLimit;
            x.MultipartHeadersLengthLimit = maxRequestLimit;
        });

        // Core Properties
        builder.Services.Configure<CoreAppSettings>(builder.Configuration.GetSection("CoreAppSettings"));
        builder.Services.AddInfrastructureServices();
        builder.Services.AddPayrollScheduleServices();
        // End Core Properties

        builder.Services.AddCors();

        //builder.Services.AddControllers();
        //builder.Services.AddControllersWithViews()
        //  .AddJsonOptions(options =>
        //  {

        //      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //      var enumConverter = new JsonStringEnumConverter();
        //      options.JsonSerializerOptions.Converters.Add(enumConverter);

        //  });

        builder.Services.AddControllersWithViews()
             .AddJsonOptions(options =>
             {

                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
                 //options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());

             }).AddOData(options =>
             {
                 options.AddRouteComponents("odata", ODataEdmBuilder.GetAppCoreEdmModel());
                 options.Select().Filter().OrderBy().SetMaxTop(20).Count().Expand();
             });


        Configuration.Setup()
          .UseSerilog(config => config
            .Message(auditEvent => auditEvent.ToJson()));

        //  TODO: Review this
        // Entity framework audit output configuration
        // Audit.Core.Configuration.Setup()
        //     .UseEntityFramework(_ => _
        //         .AuditTypeMapper(t => typeof(AuditTrail))
        //         .AuditEntityAction<object>((ev, entry, entity) =>
        //         {
        //             if (ev.EventType == "Insert" && entity is IdentityUser)
        //             {
        //                 // entry.SetEntityKProperty("CreatedBy", "user123");
        //             }
        //         }));


        builder.Services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<CreateBankCommandValidator>();
            //options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
        });

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ctx => new CommandValidationResult();
        });


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<ODataParameterOperationFilter>();

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "ChevronCoop API", Version = "1.0" });
            options.SwaggerDoc("MasterData", new OpenApiInfo { Title = "ChevronCoop API (MasterData)", Version = "1.0" });
            options.SwaggerDoc("HR", new OpenApiInfo { Title = "ChevronCoop API (HR)", Version = "1.0" });
            options.SwaggerDoc("Loans", new OpenApiInfo { Title = "ChevronCoop API (Loans)", Version = "1.0" });
            options.SwaggerDoc("Deposits", new OpenApiInfo { Title = "ChevronCoop API (Deposits)", Version = "1.0" });
            //options.SwaggerDoc("Savings", new OpenApiInfo { Title = "ChevronCoop API (Savings)", Version = "1.0" });
            options.SwaggerDoc("Docs", new OpenApiInfo { Title = "ChevronCoop API (Docs)", Version = "1.0" });
            options.SwaggerDoc("Accounting", new OpenApiInfo { Title = "ChevronCoop API (Accounting)", Version = "1.0" });
            options.SwaggerDoc("Customer", new OpenApiInfo { Title = "ChevronCoop API (Customer)", Version = "1.0" });
            options.SwaggerDoc("Employee", new OpenApiInfo { Title = "ChevronCoop API (Employee)", Version = "1.0" });
            options.SwaggerDoc("Security", new OpenApiInfo { Title = "ChevronCoop API (Security)", Version = "1.0" });
            options.SwaggerDoc("NetPay", new OpenApiInfo { Title = "ChevronCoop API (NetPay API)", Version = "1.0" });
            options.SwaggerDoc("Payroll", new OpenApiInfo { Title = "ChevronCoop API (Payroll)", Version = "1.0" });

        });

        //builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        //builder.Services.AddMediatR(typeof(LoanProductCommandHandler).Assembly);

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(LoanProductCommandHandler).Assembly);
        });


        builder.Services.AddAutoMapper(typeof(LoanProductMapperProfile).Assembly);
        //services.AddAutoMapper(typeof(DocumentTypeMapperProfile).Assembly);

        //builder.Services.AddValidatorsFromAssembly(typeof(CreateBankCommandValidator).Assembly); 
        builder.Services.AddValidatorsFromAssemblyContaining<CreateLoanProductCommandValidator>();

        builder.Services.AddHostedService<DbSeeder>();

        builder.Services.AddHttpLogging(httpLogging =>
        {
            httpLogging.LoggingFields = HttpLoggingFields.All;
            httpLogging.RequestBodyLogLimit = 4096;
            httpLogging.ResponseBodyLogLimit = 4096;
        });

        //builder.Services.AddMiniProfiler(options =>
        //{
        //    options.RouteBasePath = "/profiler";
        //    options.ColorScheme = StackExchange.Profiling.ColorScheme.Light;
        //}).AddEntityFramework();




        var app = builder.Build();

        // Use odata route debug, /$odata
        app.UseODataRouteDebug();

        // If you want to use k, enable the middleware.
        app.UseODataOpenApi();

        // Add OData /$query middleware
        app.UseODataQueryRequest();

        // Add the OData Batch middleware to support OData $Batch
        app.UseODataBatching();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            //app.UseSwagger();
            //app.UseSwaggerUI();
            // Add the OData Batch middleware to support OData $Batch

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // Set the document expantion
                c.DocExpansion(DocExpansion.None);

                // Configure how the response is displayed
                c.DefaultModelRendering(ModelRendering.Example);
                c.DefaultModelExpandDepth(0);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.EnableValidator();

                c.SwaggerEndpoint("/swagger/MasterData/swagger.json", "ChevronCoop API (MasterData)");
                c.SwaggerEndpoint("/swagger/HR/swagger.json", "ChevronCoop API (HR)");
                c.SwaggerEndpoint("/swagger/Loans/swagger.json", "ChevronCoop API (Loans)");
                c.SwaggerEndpoint("/swagger/Deposits/swagger.json", "ChevronCoop API (Deposits)");
                c.SwaggerEndpoint("/swagger/Docs/swagger.json", "ChevronCoop API (Docs)");
                c.SwaggerEndpoint("/swagger/Accounting/swagger.json", "ChevronCoop API (Accounting)");
                c.SwaggerEndpoint("/swagger/Customer/swagger.json", "ChevronCoop API (Customer)");
                c.SwaggerEndpoint("/swagger/Employee/swagger.json", "ChevronCoop API (Employee)");
                c.SwaggerEndpoint("/swagger/Security/swagger.json", "ChevronCoop API (Security)");
                c.SwaggerEndpoint("/swagger/NetPay/swagger.json", "ChevronCoop API (NetPay)");
                c.SwaggerEndpoint("/swagger/Payroll/swagger.json", "ChevronCoop API (Payroll)");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData 8.x OpenAPI");
                c.SwaggerEndpoint("/odata/$openapi", "OData raw OpenAPI");

                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseHttpLogging();
        app.UseSerilogRequestLogging(options =>
        {
            // Customize the message template
            options.MessageTemplate = "Handled {RequestPath}";

            // Emit debug-level events instead of the defaults
            options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

            // Attach additional properties to the request completion event
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                diagnosticContext.Set("QueryString", httpContext.Request.QueryString);
            };
        });

        //app.UseMiniProfiler();

        //app.MapControllers();

        app.UseRouting();

        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseSerilogRequestLogging();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseHangfireDashboard();

        //app.UseHangfireDashboard("/hangfire", new DashboardOptions()
        //{
        //    DashboardTitle = "Hangfire Dashboard",
        //    Authorization = new[]{
        //    new HangfireCustomBasicAuthenticationFilter{User = Configuration.GetSection("HangfireCredentials:UserName").Value,Pass = 
        //    Configuration.GetSection("HangfireCredentials:Password").Value}});

        //Research: https://docs.hangfire.io/en/latest/configuration/using-dashboard.html#configuring-authorization
        app.MapHangfireDashboard();
        // RecurringJob.AddOrUpdate<IBackGroundServiceManagement>(x => x.ManageApprovalNotification(), "0 * * ? * *");

        #region Approvals
        RecurringJob.AddOrUpdate<IApprovalInitiationBackgroundService>(x => x.InitiateApprovals(), Cron.MinuteInterval(1));
        #endregion

        #region Payroll Schedule Deductions
        RecurringJob.AddOrUpdate<IPayrollScheduleBackgroundService>(x => x.GetSavingDepositDeductions(),
         Cron.MinuteInterval(1));
        // special deposit payroll schedule
        RecurringJob.AddOrUpdate<IPayrollScheduleBackgroundService>(x => x.GetSpecialDepositDeductions(),
        Cron.MinuteInterval(2));

        // laon repayment payroll schedule
        RecurringJob.AddOrUpdate<IPayrollScheduleBackgroundService>(x => x.GetLoanRepaymentDeductions(),
      Cron.MinuteInterval(3));
        #endregion

        #region Interest computation
        RecurringJob.AddOrUpdate<ISDDailyInterestComputationService>(x => x.ComputeSpecialDepositDailyInterests(),
     Cron.Daily(23));
        RecurringJob.AddOrUpdate<ISDMonthlyInterestComputationService>(x => x.ComputeSpecialDepositMonthlyInterests(),
     Cron.Daily(23));

        RecurringJob.AddOrUpdate<IFixedDepositInterestComputationService>(x => x.ProcessInterestComputation(),
            Cron.Daily(23));
        #endregion

        app.Run();
    }
}
