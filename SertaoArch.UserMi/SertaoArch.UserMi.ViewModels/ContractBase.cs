using Newtonsoft.Json;

namespace SertaoArch.UserMi.Contracts
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    public class ContractBase<T>
    {
        [JsonIgnore]
        public T Id { get; set; }
    }
}
