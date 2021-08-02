using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CoolStore.AppContracts.Common;
using CoolStore.AppContracts.Dtos;
using RestEase;

namespace CoolStore.AppContracts.RestApi
{
    public interface IAppApi
    {
        [Get("api/product-api/v1/products")]
        Task<ResultDto<ListResultDto<ProductDto>>> GetProductsAsync([Header("x-query")] string xQuery);

        [Post("api/product-api/v1/products")]
        Task<ResultDto<ProductDto>> CreateProduct([Body] CreateProductModel model);
    }

    public record CreateProductModel(CreateProductDto Model);

    public class CreateProductDto
    {
        [Required] public string Name { get; set; } = default!;
        [Required] public int Quantity { get; set; }
        [Required] public decimal Cost { get; set; }
        [Required] public string ProductCodeName { get; set; } = default!;
    }
}
