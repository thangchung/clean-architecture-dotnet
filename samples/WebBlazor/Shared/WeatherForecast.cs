using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using RestEase;

namespace Blazor.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public record ResultModel<T>(T Data, bool IsError = false, string ErrorMessage = default) where T : notnull;

    public record ListResponseModel<T>(List<T> Items, long TotalItems, int Page, int PageSize) where T : notnull;

    public class QueryRequest
    {
        public List<FilterModel> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public record FilterModel(string FieldName, string Comparision, string FieldValue);

    public interface IAppApi
    {
        [Get("api/product-api/v1/products")]
        Task<ResultModel<ListResponseModel<ProductDto>>> GetProductsAsync([Header("x-query")] string xQuery);
    }
}
