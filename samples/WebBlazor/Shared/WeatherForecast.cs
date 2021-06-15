using System;
using System.Collections.Generic;

namespace Blazor.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    /*public class ResultModel<T> where T : notnull
    {
        public T Data { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }*/

    public record ResultModel<T>(T Data, bool IsError = false, string ErrorMessage = default) where T : notnull;

    public record ListResponseModel<T>(List<T> Items, long TotalItems, int Page, int PageSize) where T : notnull;
}
