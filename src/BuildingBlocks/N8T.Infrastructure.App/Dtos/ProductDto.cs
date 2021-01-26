using System;

namespace N8T.Infrastructure.App.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public DateTime Created { get; init; }
        public DateTime Modified { get; init; }
        public bool Active { get; init; }
        public int Quantity { get; init; }
        public decimal Cost { get; init; }
        public Guid ProductCodeId { get; init; }
        public string ProductCodeName { get; init; } = default!;
    }
}
