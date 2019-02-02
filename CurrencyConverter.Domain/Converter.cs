namespace CurrencyConverter.Domain
{
    public class Converter
    {
        public Amount Convert(Amount amount, Currency targetCurrency)
        {
            Rate conversionRate = new Rate(1.14m);
            return amount.Convert(targetCurrency, conversionRate);
        }
    }
}
