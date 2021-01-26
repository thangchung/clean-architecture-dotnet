using System;
using N8T.Core.Domain;

namespace ProductService.Core.Domain
{
    public class ProductCode : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private init; }
        public string Name { get; private init; } = default!;

        public static ProductCode Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static ProductCode Create(Guid id, string name)
        {
            ProductCode productCode = new() {Id = id, Name = name};

            productCode.AddDomainEvent(new ProductCodeCreated {ProductCode = productCode});

            return productCode;
        }
    }
}
