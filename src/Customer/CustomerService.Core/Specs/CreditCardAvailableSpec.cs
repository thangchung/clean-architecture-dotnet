using System;
using System.Linq.Expressions;
using CustomerService.Core.Entities;
using N8T.Core.Specification;

namespace CustomerService.Core.Specs
{
    public class CreditCardAvailableSpec : SpecificationBase<CreditCard>
    {
        private readonly DateTime _dateTime;

        public CreditCardAvailableSpec(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public override Expression<Func<CreditCard, bool>> Criteria => creditCard => creditCard.Active && creditCard.Expiry >= _dateTime;
    }
}
