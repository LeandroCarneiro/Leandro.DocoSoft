using Microsoft.Extensions.DependencyInjection;
using System;

namespace Leandro.DocoSoft.DI
{
    public static class AppContainer
    {
        private static IServiceCollection _container;

        public static IServiceCollection GetContainer()
        {
            return _container;
        }

        public static T Resolve<T>()
        {
            var serviceProvider = _container.BuildServiceProvider();
            var service = serviceProvider.GetService<T>();

            return service;
        }

        public static void SetContainer(IServiceCollection service)
        {
            if (_container == null)
                _container = service;
        }
    }
}
