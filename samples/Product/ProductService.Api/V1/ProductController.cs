using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N8T.Core.Domain;
using N8T.Infrastructure;
using N8T.Infrastructure.Controller;
using ProductService.AppCore.UseCases.Commands;
using ProductService.AppCore.UseCases.Queries;

namespace ProductService.Application.V1
{
    [Authorize("RequireInteractiveUser")]
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        [HttpGet("/api/v{version:apiVersion}/products/{id:guid}")]
        public async Task<ActionResult<ProductDto>> HandleGetProductByIdAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new GetProductById.Query { Id = id };

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("/api/v{version:apiVersion}/products")]
        public async Task<ActionResult> HandleGetProductsAsync([FromHeader(Name = "x-query")] string query,
            CancellationToken cancellationToken = new())
        {
            var queryModel = HttpContext.SafeGetListQuery<GetProducts.Query, ListResultModel<ProductDto>>(query);

            return Ok(await Mediator.Send(queryModel, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/products")]
        public async Task<ActionResult> HandleCreateProductAsync([FromBody] CreateProduct.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
