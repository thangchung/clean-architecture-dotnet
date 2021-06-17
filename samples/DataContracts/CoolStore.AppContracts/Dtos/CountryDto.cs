using System;

namespace CoolStore.AppContracts.Dtos
{
    public class CountryDto
    {
        public string Name { get; set; } = default!;
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
