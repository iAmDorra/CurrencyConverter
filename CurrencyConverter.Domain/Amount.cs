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

        public Amount Convert(Currency currency, Rate rate, Rounding rounding = Rounding.ToCents)
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
            var roundedValue = Round(convertedValue, rounding);
            return new Amount(roundedValue, currency);
        }

        private decimal Round(decimal convertedValue, Rounding rounding)
        {
            return Decimal.Round(convertedValue, 2);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Amount;
            return other != null
                   && other._value.Equals(_value)
                   && other._currency.Equals(_currency);
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
        public override string ToString()
        {
            return base.ToString() + " : value = " + this._value + " & currency = " + this._currency;
        }
    }
}