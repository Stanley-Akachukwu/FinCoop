using System;
using Microsoft.Extensions.Configuration;
using NHiLo;

namespace AP.ChevronCoop.Commons
{
    public class Utility
    {
        public static string CustomerCode = "CU";
        public static string SavingAccount = "SA";

        public static string SpecialDeposit = "SD";
        public static string FixedDeposit = "FD";
        public static string LoanApplication = "LA";
        public Utility()
        {
        }


        private static IKeyGenerator<long> GetKeyGenerator(string entityName)
        {

            IConfigurationRoot configuration;

            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //configuration = new ConfigurationBuilder()            //  .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))            //   .AddJsonFile("appsettings.json", optional: false)            //   .AddJsonFile($"appsettings.{envName}.json", optional: false)            //   .Build(); 

            var builder = new ConfigurationBuilder();


            if (string.IsNullOrWhiteSpace(envName))

            {

                configuration = builder.AddJsonFile("appsettings.json", optional: false).Build();

            }

            else
            {

                configuration = builder

              //.AddJsonFile("appsettings.json", optional: false)              .AddJsonFile($"appsettings.{envName}.json", optional: true)

              .Build();

            }


            var factory = new HiLoGeneratorFactory(configuration);

            var generator = factory.GetKeyGenerator(entityName);

            return generator;

        }


        public static long GetNextKey(string entityName)
        {
            return GetKeyGenerator(entityName).GetKey();

        }

        public static TimeSpan DateDiff(DateTimeOffset startDate, DateTimeOffset endDate) => endDate.Date.Subtract(startDate.Date);
     
    }
}

