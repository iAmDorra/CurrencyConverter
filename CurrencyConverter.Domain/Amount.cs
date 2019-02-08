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
            if (_currency.Equals(currency))
            {
                return this;
            }

            if (rate == null)
            {
                return null;
            }

            var convertedValue = rate.Multiply(_value);
            return new Amount(convertedValue, currency);
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

        public string Format(IAmountFormatter formatter)
        {
            return formatter.Format(_value, _currency);
        }

        public bool HasCurrency(Currency currency)
        {
            return _currency.Equals(currency);
        }

        public bool HasValue(decimal value)
        {
            return _value.Equals(value);
        }
    }
}