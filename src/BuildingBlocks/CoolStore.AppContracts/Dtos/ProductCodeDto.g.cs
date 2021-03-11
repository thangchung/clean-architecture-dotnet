using System;

namespace CoolStore.AppContracts.Dtos
{
    public partial class ProductCodeDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}