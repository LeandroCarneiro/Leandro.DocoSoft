using AutoMapper;
using SertaoArch.Mapping.Profiles;

namespace SertaoArch.Mapping
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