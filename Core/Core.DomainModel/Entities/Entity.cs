using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {

        [Key]
        public TKey Id { get; set; }

    }
}
