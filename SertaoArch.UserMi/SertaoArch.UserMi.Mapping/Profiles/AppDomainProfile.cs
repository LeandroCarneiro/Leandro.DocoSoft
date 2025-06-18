using AutoMapper;
using SertaoArch.UserMi.Domain;
using SertaoArch.UserMi.Domain.Entities;
using SertaoArch.UserMi.Contracts;
using SertaoArch.UserMi.Contracts.AppObject;

namespace SertaoArch.UserMi.Mapping.Profiles
{
    public class AppDomainProfile : Profile
    {
        public AppDomainProfile()
        {
            CreateMap<EntityBase<long>, ContractBase<long>>().ReverseMap();
            CreateMap<User, UserContract>().ReverseMap();
        }
    }
}
