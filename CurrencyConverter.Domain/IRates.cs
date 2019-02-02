namespace CurrencyConverter.Domain
{
    public interface IRates
    {
        Rate GetRateOf(Currency currency);
    }
}