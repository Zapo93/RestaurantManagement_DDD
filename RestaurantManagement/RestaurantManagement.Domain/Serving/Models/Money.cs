using RestaurantManagement.Common.Domain.Models;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class Money : ValueObject
    {
        public Money(double ammountInLev) 
        {
            //TODO validate value. It must not be negative.
            Value = ammountInLev;
            CurrencyAbbreviation = "BGN";
        }

        private Money() { }
        public double Value { get; set; }
        public string CurrencyAbbreviation { get; private set; }

        public void Add(Money moneyToAdd) 
        {
            if (CurrencyAbbreviation == moneyToAdd.CurrencyAbbreviation)
            {
                Value += moneyToAdd.Value;
            }
            else 
            {
                //TODO Handle different currencies or change the default currency
            }
        }

        public override string ToString() 
        {
            return $"{Value} {CurrencyAbbreviation}";
        }
    }
}
