using System;
using System.Collections.Generic;

namespace CoolStore.AppContracts.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public Guid ProductCodeId { get; set; }
        public ProductCodeDto Code { get; set; }
        public List<ReturnDto> Returns { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
