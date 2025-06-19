using System.ComponentModel.DataAnnotations.Schema;

namespace SertaoArch.Domain.Entities
{
    [Table("tbl_User")]
    public class User : EntityBase<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}