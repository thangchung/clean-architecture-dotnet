using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using CoolStore.AppContracts.RestApi;
using CoolStore.IntegrationEvents.Customer;
using CustomerService.Core.Entities;
using CustomerService.Core.Specs;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;

namespace CustomerService.Application.V1.UseCases.Commands
{
    public class CreateCustomer
    {
        public record Command : ICreateCommand<Command.CreateCustomerModel, CustomerDto>
        {
            public CreateCustomerModel Model { get; init; } = default!;

            public record CreateCustomerModel(string FirstName, string LastName, string Email, Guid CountryId);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.FirstName)
                        .NotEmpty().WithMessage("FirstName is required.")
                        .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

                    RuleFor(v => v.Model.LastName)
                        .NotEmpty().WithMessage("LastName is required.")
                        .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

                    RuleFor(v => v.Model.Email)
                        .NotEmpty().WithMessage("Email is required.")
                        .EmailAddress().WithMessage("Email should in email format.")
                        .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<CustomerDto>>
            {
                private readonly IRepository<Customer> _customerRepository;
                private readonly ICountryApi _countryApi;

                public Handler(IRepository<Customer> customerRepository, ICountryApi countryApi)
                {
                    _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                    _countryApi = countryApi ?? throw new ArgumentNullException(nameof(countryApi));
                }

                public async Task<ResultModel<CustomerDto>> Handle(Command request,
                    CancellationToken cancellationToken)
                {
                    var alreadyRegisteredSpec = new CustomerAlreadyRegisteredSpec(request.Model.Email);

                    var existingCustomer = await _customerRepository.FindOneAsync(alreadyRegisteredSpec);

                    if (existingCustomer != null)
                    {
                        throw new Exception("Customer with this email already exists");
                    }

                    // check country is exists and valid
                    var (countryDto, isError, _) = await _countryApi.GetCountryByIdAsync(request.Model.CountryId);
                    if (isError || countryDto.Id.Equals(Guid.Empty))
                    {
                        throw new Exception("Country Id is not valid.");
                    }

                    var customer = Customer.Create(request.Model.FirstName, request.Model.LastName, request.Model.Email, request.Model.CountryId);

                    customer.AddDomainEvent(new CustomerCreatedIntegrationEvent());

                    var created = await _customerRepository.AddAsync(customer);

                    return ResultModel<CustomerDto>.Create(created.AdaptToDto());
                }
            }
        }
    }
}
