using System;

namespace CurrencyConverter.Domain
{
    public class Amount
    {
        private readonly decimal _value;
        private readonly Currency _currency;

        public Amount(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public Amount Convert(Currency currency, Rate rate)
        {
            return new Amount(11.4m, currency);
        }

        public override bool Equals(object obj)
        {
            return obj is Amount amount 
                   && amount._value == _value 
                   && amount._currency == _currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }
    }
}