using System;
using System.Collections.Generic;

namespace N8T.Core.Domain
{
    public interface IAggregateRoot { }

    public interface ITxRequest { }

    public abstract class EntityRootBase :  EntityBase, IAggregateRoot
    {
        public HashSet<EventBase> DomainEvents { get; private set; }

        public void AddDomainEvent(EventBase eventItem)
        {
            DomainEvents ??= new HashSet<EventBase>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(EventBase eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }

    public abstract class EntityBase
    {
        public Guid Id { get; protected init; } = Guid.NewGuid();
        public DateTime Created { get; protected init; } = DateTime.UtcNow;
        public DateTime? Updated { get; protected set; }
    }
}
