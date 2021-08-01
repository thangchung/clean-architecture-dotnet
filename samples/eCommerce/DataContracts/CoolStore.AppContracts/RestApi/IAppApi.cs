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
    }
}
