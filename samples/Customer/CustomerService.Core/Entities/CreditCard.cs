using System;
using System.Linq;
using N8T.Core.Domain;

namespace CustomerService.Core.Entities
{
    public class CreditCard : EntityRootBase
    {
        public string NameOnCard { get; protected set; } = default!;
        public string CardNumber { get; protected set; } = default!;
        public bool Active { get; protected set; }
        public DateTime Expiry { get; protected set; }
        public Customer Customer { get; protected set; }

        public static CreditCard Create(Customer customer, string name, string cardNum, DateTime expiry)
        {
            if (customer == null)
                throw new Exception("Customer object can't be null");

            if (string.IsNullOrEmpty(name))
                throw new Exception("Name can't be empty");

            if (string.IsNullOrEmpty(cardNum) || cardNum.Length < 6)
                throw new Exception("Card number length is incorrect");

            if (DateTime.Now > expiry)
                throw new Exception("Credit card expiry can't be in the past");

            CreditCard creditCard = new()
            {
                Customer = customer,
                NameOnCard = name,
                CardNumber = cardNum,
                Expiry = expiry,
                Active = true,
                Created = DateTime.Today
            };

            if(customer.CreditCards.Contains(creditCard))
                throw new Exception("Can't add same card to the collection");

            return creditCard;
        }
    }
}
