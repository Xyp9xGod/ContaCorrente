using System;
using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; protected set; }
    }
}
