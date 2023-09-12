using AP.ChevronCoop.Entities;
using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChevronCoop.API.Config
{
    public class ChevronCoopDbContextFactory : IDesignTimeDbContextFactory<ChevronCoopDbContext>
    {
        public ChevronCoopDbContext CreateDbContext(string[] args)
        {
            var envLoader = new EnvLoader().Load();

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = null;

            if (string.IsNullOrEmpty(environment))
            {
                configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              //.AddJsonFile($"appsettings.{environment}.json", optional: true)
              .AddEnvironmentVariables()
              .Build();
            }
            else
            {
                configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
              //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{environment}.json", optional: false)
              .AddEnvironmentVariables()
              .Build();
            }



            var dbContextBuilder = new DbContextOptionsBuilder<ChevronCoopDbContext>();

            var connectionString = configuration.GetConnectionString("chevroncoop");

            dbContextBuilder.UseSqlServer(connectionString);

            return new ChevronCoopDbContext(dbContextBuilder.Options);
        }
    }

}