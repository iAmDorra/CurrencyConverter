using CurrencyConverter.Domain;
using System;
using System.Linq;

namespace CurrencyConverter.Infrastructure
{
    public class Rates : IRates
    {
        public Rate GetRateOf(Currency currency)
        {
            using (var db = new CurrencyConverterContext())
            {
                var rateValue = db.Rates.FirstOrDefault(r => currency.Is(r.Currency));
                return rateValue == null ? null : new Rate(rateValue.Value);
            }
        }
    }
}
