﻿using SertaoArch.Data.Contexts;
using SertaoArch.DI;
using SertaoArch.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace SertaoArch.Bootstrap
{
    public static class DIBootstrap
    {
        public static void RegisterTypes(IServiceCollection service, IConfiguration configuration)
        {
            service.RegisterApp()
                .RegisterRepository()
                .RegisterService()
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