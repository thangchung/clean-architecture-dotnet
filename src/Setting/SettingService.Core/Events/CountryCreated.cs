using N8T.Core.Domain;
using SettingService.Core.Entities;

namespace SettingService.Core.Events
{
    public class CountryCreated : DomainEventBase
    {
        public Country Country { get; set; } = null!;

        public override void Flatten()
        {
            MetaData.Add("Id", Country.Id);
            MetaData.Add("Name", Country.Name);
        }
    }
}
