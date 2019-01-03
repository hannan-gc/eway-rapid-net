using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    public abstract class SdkTestBase
    {
        public static IConfiguration Configuration = GetConfigurations();
        public static string PASSWORD = Configuration["PASSWORD"];
        public static string APIKEY = Configuration["APIKEY"];
        public static string ENDPOINT = Configuration["ENDPOINT"];
        public static int APIVERSION = int.Parse(Configuration["APIVERSION"]);

        protected IRapidClient CreateRapidApiClient()
        {
            var client = RapidClientFactory.NewRapidClient(APIKEY, PASSWORD, ENDPOINT);
            client.SetVersion(GetVersion());
            return client;
        }

        protected int GetVersion()
        {
            string version = System.Environment.GetEnvironmentVariable("APIVERSION");
            int v;
            if (version != null && int.TryParse(version, out v))
            {
                return v;
            }
            return APIVERSION;
        }

        public static IConfiguration GetConfigurations()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
    }
}
