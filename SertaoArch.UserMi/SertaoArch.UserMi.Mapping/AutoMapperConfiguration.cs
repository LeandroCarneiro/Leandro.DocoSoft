using AutoMapper;
using SertaoArch.UserMi.Mapping.Profiles;

namespace SertaoArch.UserMi.Mapping
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