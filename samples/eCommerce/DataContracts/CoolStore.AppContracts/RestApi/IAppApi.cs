using System;
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
        Task<ResultDto<ProductDto>> CreateProductAsync([Body] CreateProductContainer model);

        [Get("api/product-api/v1/products/{id}")]
        Task<ResultDto<ProductDto>> GetProductAsync([Path] Guid id);
    }

    public record CreateProductContainer(CreateProductModel Model);

    public class CreateProductModel
    {
        [Required] public string Name { get; set; } = default!;
        [Required] public int Quantity { get; set; }
        [Required] public decimal Cost { get; set; }
        [Required] public string ProductCodeName { get; set; } = default!;
    }

    public class EditProductModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; } = default!;
        [Required] public int Quantity { get; set; }
        [Required] public decimal Cost { get; set; }
        [Required] public string ProductCodeName { get; set; } = default!;
    }
}
