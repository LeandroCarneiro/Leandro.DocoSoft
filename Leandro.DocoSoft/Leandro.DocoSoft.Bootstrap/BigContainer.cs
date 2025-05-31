using Leandro.DocoSoft.Application.Domain;
using Leandro.DocoSoft.Business.Domain;
using Leandro.DocoSoft.Data.Contexts;
using Leandro.DocoSoft.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.DocoSoft.Bootstrap
{
    public static class BigContainer
    {
        public static IServiceCollection RegisterApp(this IServiceCollection service)
        {
            service.AddTransient<UserApp>();
            return service;
        }

        public static IServiceCollection RegisterRepository(this IServiceCollection service)
        {
            service.AddTransient<IUserRepository, UserRepository>();

            return service;
        }

        public static IServiceCollection RegisterPersistence(this IServiceCollection service)
        {
            service.AddDbContext<AppDbContext>();
            service.AddTransient<IDbContext, AppDbContext>();

            return service;
        }

        public static IServiceCollection RegisterPersistenceTest(this IServiceCollection service)
        {
            service.AddDbContext<MockDb>();
            service.AddTransient<IDbContext, MockDb>();

            return service;
        }
    }
}
