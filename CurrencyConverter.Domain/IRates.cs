using CurrencyConverter.Domain.TourAttributes;

namespace CurrencyConverter.Domain
{
    public interface IRates
    {
        [Step("Conversion", "Find rates in real time in external market sources", 1)]
        Rate GetRateOf(Currency currency);
    }
}