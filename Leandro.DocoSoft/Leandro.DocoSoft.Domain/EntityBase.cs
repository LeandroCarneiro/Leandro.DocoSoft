using System;
using System.ComponentModel.DataAnnotations;

namespace Leandro.DocoSoft.Domain
{
    public interface IEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        //UserApp CreatedBy { get; set; }
        //UserApp ModifiedBy { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        [Key]
        T Id { get; set; }
    }

    public class EntityBase<T> : IEntity<T>
    {
        public T Id { get; set; }
        //public long CreaterId { get; set; }
        //public long? ModifierId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //[ForeignKey(nameof(CreaterId))]
        //public virtual UserApp CreatedBy { get; set; }
        //[ForeignKey(nameof(ModifierId))]
        //public virtual UserApp ModifiedBy { get; set; }
    }
}
