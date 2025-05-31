using AutoMapper;

namespace Leandro.DocoSoft.Mapping
{
    public class MappingWraper
    {
        public static TTo Map<TFrom, TTo>(TFrom obj)
        {
            return new Mapper(AutoMapperConfiguration._config).Map<TFrom, TTo>(obj);
        }
    }
}
