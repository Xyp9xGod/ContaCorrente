using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; protected set; }
    }
}
