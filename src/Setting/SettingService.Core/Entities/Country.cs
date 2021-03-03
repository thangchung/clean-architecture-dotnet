using System;
using N8T.Core.Domain;
using SettingService.Core.Events;

namespace SettingService.Core.Entities
{
    public class Country : EntityBase, IAggregateRoot
    {
        public string Name { get; protected set; } = default!;

        public static Country Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static Country Create(Guid id, string name)
        {
            Country country = new()
            {
                Id = id,
                Name = name
            };

            country.AddDomainEvent(new CountryCreated {Country = country});

            return country;
        }
    }
}
