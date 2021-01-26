using N8T.Core.Domain;

namespace ProductService.Core.Domain
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
