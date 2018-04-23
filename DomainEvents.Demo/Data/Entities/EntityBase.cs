using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace DomainEvents.Demo.Data.Entities
{
    public abstract class EntityBase
        {
        [NotMapped]
        private List<INotification> _domainEvents;
        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
            {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
            }

        public void RemoveDomainEvent(INotification eventItem)
            {
            _domainEvents?.Remove(eventItem);
            }

        public void ClearDomainEvents()
            {
            _domainEvents?.Clear();
            }
    }
    public class EntityBase<T> : EntityBase
        {
        [Key]
        public T Id { get; set; }
        }
   
}
