namespace SertaoArch.UserMi.Contracts.AppObject
{
    public class UserContract : ContractBase<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
