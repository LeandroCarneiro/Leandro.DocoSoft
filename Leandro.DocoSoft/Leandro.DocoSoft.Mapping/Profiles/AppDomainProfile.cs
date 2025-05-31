using AutoMapper;
using Leandro.DocoSoft.Domain;
using Leandro.DocoSoft.Domain.Entities;
using Leandro.DocoSoft.Contracts;
using Leandro.DocoSoft.Contracts.AppObject;

namespace Leandro.DocoSoft.Mapping.Profiles
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
