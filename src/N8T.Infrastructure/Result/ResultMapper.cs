using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace N8T.Infrastructure.Result
{
    public static class ResultMapper
    {
        public static IResult BadRequest() => new StatusCodeResult(StatusCodes.Status400BadRequest);

        public static IResult NotFound() => new StatusCodeResult(StatusCodes.Status404NotFound);

        public static IResult NoContent() => new StatusCodeResult(StatusCodes.Status204NoContent);

        public static OkResult<T> Ok<T>(T value) => new(value);

        public class OkResult<T> : IResult
        {
            private readonly T _value;

            public OkResult(T value)
            {
                _value = value;
            }

            public Task ExecuteAsync(HttpContext httpContext)
            {
                return httpContext.Response.WriteAsJsonAsync(_value);
            }
        }
    }
}
