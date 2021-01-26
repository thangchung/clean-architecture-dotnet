using System;
using N8T.Core.Domain;

namespace ProductService.Core.Domain
{
    public class Return : EntityBase
    {
        public Product Product { get; protected set; }
        public Guid CustomerId { get; set; }
        public ReturnReason Reason { get; protected set; }
        public string Note { get; protected set; } = default!;
    }
}
