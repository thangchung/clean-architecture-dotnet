using System.Threading;
using System.Threading.Tasks;
using CustomerService.AppCore.UseCases.Commands;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Controller;

namespace CustomerService.Application.V1
{
    [ApiVersion("1.0")]
    public class CustomerController : BaseController
    {
        [ApiVersion( "1.0" )]
        [HttpPost("/api/v{version:apiVersion}/customers")]
        public async Task<ActionResult> HandleAsync([FromBody] CreateCustomer.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
