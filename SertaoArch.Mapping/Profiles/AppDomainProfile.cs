using AutoMapper;
using SertaoArch.Domain;
using SertaoArch.Domain.Entities;
using SertaoArch.Contracts;
using SertaoArch.Contracts.AppObject;

namespace SertaoArch.Mapping.Profiles
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
