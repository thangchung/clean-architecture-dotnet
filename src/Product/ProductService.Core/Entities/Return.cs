using System;
using N8T.Core.Domain;

namespace ProductService.Core.Entities
{
    public class Return : EntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; protected set; }
        public Guid CustomerId { get; set; }
        public ReturnReason Reason { get; protected set; }
        public string Note { get; protected set; } = default!;
    }
}
