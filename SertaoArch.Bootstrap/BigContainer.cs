using Microsoft.Extensions.DependencyInjection;
using SertaoArch.Business.Domain;
using SertaoArch.Data.Contexts;
using SertaoArch.Domain.Interfaces;
using SertaoArch.QueueServiceRMQ;
using SertaoArch.UserMi.Application.Domain;
using SertaoArch.UserMi.Application.Interface;

namespace SertaoArch.Bootstrap
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

        public static IServiceCollection RegisterService(this IServiceCollection service)
        {
            service.AddTransient<IQueueService, QueueService>();
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
