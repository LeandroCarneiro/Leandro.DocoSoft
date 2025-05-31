using Leandro.DocoSoft.Data.Contexts;
using Leandro.DocoSoft.DI;
using Leandro.DocoSoft.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.DocoSoft.Bootstrap
{
    public static class DIBootstrap
    {
        public static void RegisterTypes(IServiceCollection service)
        {
            service.RegisterApp()
                .RegisterRepository()
                .RegisterPersistence();

            AppContainer.SetContainer(service);
            AutoMapperConfiguration.Register();

            Migrate(service);
        }

        public static void RegisterTypesTest(IServiceCollection service)
        {
            service.RegisterApp()
                .RegisterRepository()
                .RegisterPersistenceTest();

            AppContainer.SetContainer(service);
            AutoMapperConfiguration.Register();
        }

        private static void Migrate(IServiceCollection services)
        {
            var dao = services.BuildServiceProvider().GetService<AppDbContext>();
            dao.Database.EnsureCreated();
        }
    }
}