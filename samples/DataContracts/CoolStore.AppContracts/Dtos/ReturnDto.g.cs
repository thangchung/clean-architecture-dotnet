using System;
using CoolStore.AppContracts.Dtos;

namespace CoolStore.AppContracts.Dtos
{
    public partial class ReturnDto
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