using System;

namespace CoolStore.AppContracts.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Guid CountryId { get; set; }
    }
}
