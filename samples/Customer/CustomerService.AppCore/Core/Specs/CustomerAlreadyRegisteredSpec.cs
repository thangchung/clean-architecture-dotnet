using System;
using System.Linq.Expressions;
using CustomerService.AppCore.Core.Entities;
using N8T.Core.Specification;

namespace CustomerService.AppCore.Core.Specs
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
