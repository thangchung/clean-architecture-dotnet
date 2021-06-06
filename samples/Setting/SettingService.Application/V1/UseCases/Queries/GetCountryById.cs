using System;
using System.Collections.Generic;
using CoolStore.AppContracts.Dtos;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;
using SettingService.Core.Entities;

namespace SettingService.Application.V1.UseCases.Queries
{
    public class GetCountryById
    {
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
