using SertaoArch.UserMi.Common.Exceptions;
using SertaoArch.UserMi.Contracts;
using SertaoArch.UserMi.Domain;
using SertaoArch.UserMi.Domain.Interfaces;
using SertaoArch.UserMi.Mapping;

namespace SertaoArch.UserMi.Application.Domain
{
    public abstract class BaseApp<T_VW, T>
        where T_VW : ContractBase<long>
        where T : EntityBase<long>
    {
        protected readonly ICrud<T, long> _repo;
        public BaseApp(ICrud<T, long> repo)
        {
            _repo = repo;
        }

        public virtual async Task<T_VW> Get(long id, CancellationToken cancellation)
        {
            var entity = await _repo.Get(id, cancellation);
            return Resolve(entity);
        }
        protected T_VW Resolve(T entity)
        {
            if (entity == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<T, T_VW>(entity);
        }
        protected T Resolve(T_VW viewModel)
        {
            if (viewModel == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<T_VW, T>(viewModel);
        }
    }
}
