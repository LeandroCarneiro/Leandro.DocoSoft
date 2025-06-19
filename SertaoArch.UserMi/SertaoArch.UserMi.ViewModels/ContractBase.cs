using System.Text.Json.Serialization;

namespace SertaoArch.Contracts
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
