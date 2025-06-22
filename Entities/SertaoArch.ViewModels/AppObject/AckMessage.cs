namespace SertaoArch.Contracts.AppObject
{
    public class AckMessage : ContractBase<long>
    {
        public string Message { get; set; }
    }
}
