using RestaurantManagement.Domain.Common;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class Money : ValueObject
    {
        internal Money(double ammountInLev) 
        {
            //TODO validate value. It must not be negative.
            Value = ammountInLev;
            CurrencyAbbreviation = "BGN";
        }

        public double Value { get; private set; }
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
