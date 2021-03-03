using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace N8T.Infrastructure.Endpoint
{
    public abstract class BaseAsyncEndpoint
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : Ardalis.ApiEndpoints.BaseAsyncEndpoint.WithRequest<TRequest>
                .WithResponse<TResponse>
            {
                private ISender _mediator;

                protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
            }

            public abstract class WithoutResponse : Ardalis.ApiEndpoints.BaseAsyncEndpoint.WithRequest<TRequest>
                .WithoutResponse
            {
                private ISender _mediator;

                protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : Ardalis.ApiEndpoints.BaseAsyncEndpoint.WithoutRequest
                .WithResponse<TResponse>
            {
                private ISender _mediator;

                protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
            }

            public abstract class WithoutResponse : Ardalis.ApiEndpoints.BaseAsyncEndpoint.WithoutRequest
                .WithoutResponse
            {
                private ISender _mediator;

                protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
            }
        }
    }
}
