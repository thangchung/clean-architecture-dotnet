using System;

namespace CoolStore.AppContracts.Dtos
{
    public class CountryDto
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
