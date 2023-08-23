using CurrencyConverter.Domain;
using CurrencyConverter.Infrastructure;

namespace CurrencyConverter.Web.Controllers
{
    public class ConversionService
    {
        public string Convert(string amountValue, string currencyName)
        {
            var currency = new Currency(currencyName);
            var converter = new Converter(new Rates());
            decimal amount = decimal.Parse(amountValue);
            Currency eurCurrency = new Currency("EUR");
            Amount amountToConvert = new Amount(amount, eurCurrency);
            var convertedAmount = converter.Convert(amountToConvert, currency);

            if (convertedAmount == null)
            {
                return "Error : impossible to convert the amount.";
            }

            var formatter = new AmountFormatter();
            return convertedAmount.Format(formatter);
        }
    }
}
