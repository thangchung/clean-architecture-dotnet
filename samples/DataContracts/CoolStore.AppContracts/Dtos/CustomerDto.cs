using System;

namespace CoolStore.AppContracts.Dtos
{
    public class CustomerDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public decimal Balance { get; set; }
        public Guid CountryId { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
