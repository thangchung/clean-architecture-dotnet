using System;
using System.Linq.Expressions;
using CustomerService.Core.Entities;
using N8T.Core.Specification;

namespace CustomerService.Core.Specs
{
    public class CustomerAlreadyRegisteredSpec : SpecificationBase<Customer>
    {
        private readonly string _email;

        public CustomerAlreadyRegisteredSpec(string email)
        {
            _email = email;
        }

        public override Expression<Func<Customer, bool>> Criteria => customer => customer.Email == _email;
    }
}
