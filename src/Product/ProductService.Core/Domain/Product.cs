using System;
using System.Collections.Generic;
using N8T.Core.Domain;

namespace ProductService.Core.Domain
{
    public class Product : EntityBase, IAggregateRoot
    {
        private readonly List<Return> _returns = new();

        public Guid Id { get; private init; }
        public string Name { get; private init; } = default!;
        public bool Active { get; private init; }
        public int Quantity { get; private init; }
        public decimal Cost { get; private init; }
        public ProductCode Code { get; private init; } = default!;

        public IEnumerable<Return> Returns
        {
            get
            {
                return _returns.AsReadOnly();
            }
        }

        public static Product Create(string name, int quantity, decimal cost, ProductCode productCode)
        {
            return Create(Guid.NewGuid(), name, quantity, cost, productCode);
        }

        public static Product Create(Guid id, string name, int quantity, decimal cost, ProductCode productCode)
        {
            Product product = new()
            {
                Id = id,
                Name = name,
                Quantity = quantity,
                Created = DateTime.Now,
                Active = true,
                Cost = cost,
                Code = productCode
            };

            product.AddDomainEvent(new ProductCreated {Product = product});

            return product;
        }
    }
}
