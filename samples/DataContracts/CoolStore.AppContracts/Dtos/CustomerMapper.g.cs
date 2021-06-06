using System;
using System.Linq.Expressions;
using CoolStore.AppContracts.Dtos;
using CustomerService.Core.Entities;

namespace CoolStore.AppContracts.Dtos
{
    public static partial class CustomerMapper
    {
        public static CustomerDto AdaptToDto(this Customer p1)
        {
            return p1 == null ? null : new CustomerDto()
            {
                FirstName = p1.FirstName,
                LastName = p1.LastName,
                Email = p1.Email,
                Balance = p1.Balance,
                CountryId = p1.CountryId,
                Id = p1.Id,
                Created = p1.Created,
                Updated = p1.Updated
            };
        }
        public static CustomerDto AdaptTo(this Customer p2, CustomerDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CustomerDto result = p3 ?? new CustomerDto();
            
            result.FirstName = p2.FirstName;
            result.LastName = p2.LastName;
            result.Email = p2.Email;
            result.Balance = p2.Balance;
            result.CountryId = p2.CountryId;
            result.Id = p2.Id;
            result.Created = p2.Created;
            result.Updated = p2.Updated;
            return result;
            
        }
        public static Expression<Func<Customer, CustomerDto>> ProjectToDto => p4 => new CustomerDto()
        {
            FirstName = p4.FirstName,
            LastName = p4.LastName,
            Email = p4.Email,
            Balance = p4.Balance,
            CountryId = p4.CountryId,
            Id = p4.Id,
            Created = p4.Created,
            Updated = p4.Updated
        };
    }
}