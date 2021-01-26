using System.Collections.Generic;
using N8T.Core.Domain;

namespace N8T.Core.Repository
{
    public interface IDomainEventRepository
    {
        void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent;
        IEnumerable<DomainEventRecord> FindAll();
    }
}
