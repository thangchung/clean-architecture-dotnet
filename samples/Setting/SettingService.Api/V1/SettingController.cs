using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Controller;
using SettingService.AppCore.UseCases.Queries;

namespace SettingService.Application.V1
{
    [ApiVersion("1.0")]
    public class SettingController : BaseController
    {
        [ApiVersion( "1.0" )]
        [HttpGet("/api/v{version:apiVersion}/countries/{id:guid}")]
        public async Task<ActionResult<CountryDto>> HandleAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new GetCountryById.Query {Id = id};

            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
