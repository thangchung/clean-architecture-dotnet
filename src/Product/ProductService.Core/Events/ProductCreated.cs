using N8T.Core.Domain;
using ProductService.Core.Entities;

namespace ProductService.Core.Events
{
    public class ProductCreated : DomainEventBase
    {
        public Product Product { get; init; } = null!;

        public override void Flatten()
        {
            MetaData.Add("ProductId", Product.Id);
            MetaData.Add("ProductName", Product.Name);
            MetaData.Add("ProductQuantity", Product.Quantity);
            MetaData.Add("ProductCode", Product.Code.Id);
            MetaData.Add("ProductCost", Product.Cost);
        }
    }
}
