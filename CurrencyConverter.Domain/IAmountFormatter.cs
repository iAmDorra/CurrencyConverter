namespace CurrencyConverter.Domain
{
    public interface IAmountFormatter
    {
        string Format(decimal amount, Currency currency);
    }
}