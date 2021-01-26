using N8T.Core.Domain;
using ProductService.Core.Entities;

namespace ProductService.Core.Events
{
    public class ProductCodeCreated : DomainEventBase
    {
        public ProductCode ProductCode { get; init; } = null!;

        public override void Flatten()
        {
            MetaData.Add("ProductCodeId", ProductCode.Id);
            MetaData.Add("ProductCodeName", ProductCode.Name);
        }
    }
}
