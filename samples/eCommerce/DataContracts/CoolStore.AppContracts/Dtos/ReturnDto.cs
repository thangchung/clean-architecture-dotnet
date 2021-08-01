using System;

namespace CoolStore.AppContracts.Dtos
{
    public class ReturnDto
    {
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; } = default!;
        public Guid CustomerId { get; set; }
        public string Reason { get; set; } = default!;
        public string Note { get; set; } = default!;
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
