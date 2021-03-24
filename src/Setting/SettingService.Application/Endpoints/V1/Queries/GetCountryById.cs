using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using N8T.Core.Domain;
using N8T.Core.Repository;
using N8T.Infrastructure.Endpoint;
using SettingService.Core.Entities;

namespace SettingService.Application.Endpoints.V1.Queries
{
    public class GetCountryById : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<CountryDto>
    {
        [ApiVersion( "1.0" )]
        [HttpGet("/api/v{version:apiVersion}/countries/{id:guid}")]
        public override async Task<ActionResult<CountryDto>> HandleAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new Query {Id = id};

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        public record Query : IItemQuery<Guid, CountryDto>
        {
            public List<string> Includes { get; init; } = new();
            public Guid Id { get; init; }

            internal class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(x => x.Id)
                        .NotNull()
                        .NotEmpty().WithMessage("Id is required.");
                }
            }

            internal class Handler : RequestHandler<Query, ResultModel<CountryDto>>
            {
                private readonly IRepository<Country> _countryRepository;

                public Handler(IRepository<Country> countryRepository)
                {
                    _countryRepository =
                        countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
                }

                protected override ResultModel<CountryDto> Handle(Query request)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var country = _countryRepository.FindById(request.Id);

                    return ResultModel<CountryDto>.Create(country.AdaptToDto());
                }
            }
        }
    }
}
