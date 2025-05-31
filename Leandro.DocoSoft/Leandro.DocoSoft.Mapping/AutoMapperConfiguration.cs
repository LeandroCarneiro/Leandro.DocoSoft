using AutoMapper;
using Leandro.DocoSoft.Mapping.Profiles;

namespace Leandro.DocoSoft.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration _config;

        public static void Register()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppDomainProfile());
            });
        }
    }
}