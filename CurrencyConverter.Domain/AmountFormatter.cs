namespace CurrencyConverter.Domain
{
    public class AmountFormatter : IAmountFormatter
    {
        public string Format(decimal amount, Currency currency)
        {
            return $"{amount} {currency}";
        }
    }
}