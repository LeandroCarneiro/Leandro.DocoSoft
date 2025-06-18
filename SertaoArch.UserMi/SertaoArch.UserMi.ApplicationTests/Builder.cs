using SertaoArch.UserMi.Bootstrap;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

[assembly: FunctionsStartup(typeof(SertaoArch.UserMi.ApplicationTests.Builder))]

namespace SertaoArch.UserMi.ApplicationTests
{
    public static class Builder
    {
        static IServiceCollection _container;

        public static void Setup()
        {
            if (_container == null)
            {
                _container = new ServiceCollection();
                _container.AddSingleton(GetConfiguration());
                _container.AddSingleton<ILoggerFactory>();

                DIBootstrap.RegisterTypesTest(_container);
            }
        }

        public static IConfiguration GetConfiguration()
        {
            var current = Directory.GetCurrentDirectory();
            var directories = Directory.GetParent(current)
                .Parent.Parent.Parent.GetDirectories();

            var config = directories.Where(x => x.FullName.Contains("Api"))?.First()?.FullName;

            return new ConfigurationBuilder()
                .SetBasePath(config)
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
