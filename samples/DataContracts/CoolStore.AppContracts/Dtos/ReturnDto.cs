using System;

namespace CoolStore.AppContracts.Dtos
{
    public class ReturnDto
    {
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public Guid CustomerId { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
