using System;
using System.Collections.Generic;

namespace N8T.Core.Domain
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected init; } = Guid.NewGuid();
        public DateTime Created { get; protected init; } = DateTime.UtcNow;
        public DateTime? Updated { get; protected set; }
        public HashSet<DomainEventBase> DomainEvents { get; private set; }

        protected void AddDomainEvent(DomainEventBase eventItem)
        {
            DomainEvents ??= new HashSet<DomainEventBase>();
            DomainEvents.Add(eventItem);
        }

        protected void RemoveDomainEvent(DomainEventBase eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }
}
