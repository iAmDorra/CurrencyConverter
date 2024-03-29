﻿namespace CurrencyConverter.Domain
{
    public class Converter
    {
        private readonly IRates _rates;

        public Converter(IRates rates)
        {
            _rates = rates;
        }

        public Amount Convert(Amount amount, Currency targetCurrency, Rounding rounding = Rounding.ToCents)
        {
            Rate conversionRate = _rates.GetRateOf(targetCurrency);
            return amount.Convert(targetCurrency, conversionRate, rounding);
        }
    }

    public enum Rounding
    {
        ToCents,
        ToUnits
    }
}
