using Microsoft.Extensions.Configuration;
using NHiLo;

namespace AP.ChevronCoop.AppCore.Services.Helpers;

public class NHiloHelper
{
    public static string CustomerCode = "CU";
    public static string SavingAccount = "SA";
    public static string SpecialDeposit = "SD";
    public static string FixedDeposit = "FD";
    public static string LoanApplication = "LA";

    private static IKeyGenerator<long> GetKeyGenerator(string entityName)
    {
        IConfigurationRoot configuration;
        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      //  envName = "Staging";
        if (string.IsNullOrWhiteSpace(envName))
            configuration = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
              .AddJsonFile("appsettings.json", optional: false)
              .Build();
        else
            configuration = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
              .AddJsonFile("appsettings.json", optional: false)
              .AddJsonFile($"appsettings.{envName}.json", optional: false)
              .Build();

        var factory = new HiLoGeneratorFactory(configuration);
        var generator = factory.GetKeyGenerator(entityName);
        return generator;
    }

    public static long GetNextKey(string entityName)
    {
        return GetKeyGenerator(entityName).GetKey();
    }
}