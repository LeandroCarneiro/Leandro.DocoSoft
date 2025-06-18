using SertaoArch.UserMi.Application.Domain;
using SertaoArch.UserMi.Business.Domain;
using SertaoArch.UserMi.Data.Contexts;
using SertaoArch.UserMi.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SertaoArch.UserMi.Application.Interface;
using SertaoArch.QueueServiceRMQ;

namespace SertaoArch.UserMi.Bootstrap
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
