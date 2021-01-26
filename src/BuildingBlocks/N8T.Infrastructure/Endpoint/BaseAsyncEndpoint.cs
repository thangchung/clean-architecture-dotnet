using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace N8T.Infrastructure.Endpoint
{
    public abstract class BaseAsyncEndpoint<TRequest, TResponse>
        : Ardalis.ApiEndpoints.BaseAsyncEndpoint<TRequest, TResponse>
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
