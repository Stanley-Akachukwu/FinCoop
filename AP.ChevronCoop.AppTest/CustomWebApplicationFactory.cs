using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace AP.ChevronCoop.AppTest;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration(configurationBuilder =>
    {
      var integrationConfig = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

      configurationBuilder.AddConfiguration(integrationConfig);
    });

    builder.ConfigureServices((builder, services) =>
    {
      // services
      //   .Remove<ICurrentUserService>()
      //   .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
      //     s.UserId == GetCurrentUserId()));

      // services
      //   .Remove<DbContextOptions<ChevronCoopDbContext>>()
      //   .AddDbContext<ChevronCoopDbContext>((sp, options) =>
      //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
      //       builder => builder.MigrationsAssembly(typeof(ChevronCoopDbContext).Assembly.FullName)));
    });
  }
}