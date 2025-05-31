using Leandro.DocoSoft.Application.Domain;
using Leandro.DocoSoft.Business.Domain;
using Leandro.DocoSoft.Data.Contexts;
using Leandro.DocoSoft.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.DocoSoft.Bootstrap
{
    public static class BigContainer
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection service)
        {
            service.AddTransient<UserApp>();
            return service;
        }

        public static IServiceCollection RegisterAppBusiness(this IServiceCollection service)
        {
            service.AddTransient<IUserRepository, UserRepository>();

            return service;
        }

        public static IServiceCollection RegisterAppPersistence(this IServiceCollection service)
        {
            service.AddDbContext<AppDbContext>();
            service.AddTransient<IDbContext, AppDbContext>();

            return service;
        }

        public static IServiceCollection RegisterAppPersistenceTest(this IServiceCollection service)
        {
            service.AddDbContext<MockDb>();
            service.AddTransient<IDbContext, MockDb>();

            return service;
        }
    }
}
